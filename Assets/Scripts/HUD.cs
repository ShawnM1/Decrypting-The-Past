﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class HUD : MonoBehaviour {
    static Text BottomUIText;
    static Text TopUIText;
    static Animator animator;
    private GameObject pauseMenu;
    private static string previousBottomText;
    private static bool showingWrongText = false;
    public static InputField inputField;
    static GameObject InputCanvas;
    static Button InputButton;
    static Button ActionButton;
    static Text InfoText;
    static GameObject infoBox;
    static GameObject victoryScreen;
    static bool pauseable = true;
    public static bool Hidden = false;
    static List<GameObject> States = new List<GameObject>();
    static Button tutorialButton; 
	// Use this for initialization
	void Start () {
        BottomUIText = this.transform.Find("UIDisplayText").GetComponent<Text>();
        TopUIText = this.transform.Find("UITopText").GetComponent<Text>();
        animator = GetComponent<Animator>();
        pauseMenu = this.transform.Find("PauseMenu").gameObject;
        inputField = this.transform.Find("KeyInputCanvas/HUDInputText").GetComponent<InputField>();
        InputButton = this.transform.Find("KeyInputCanvas/InputTextButton").GetComponent<Button>();
        InputCanvas = this.transform.Find("KeyInputCanvas").gameObject;
        ActionButton = this.transform.Find("ActionButton").GetComponent<Button>();
        infoBox = this.transform.Find("InfoBox").gameObject;
        InfoText = this.transform.Find("InfoBox/InfoText").GetComponent<Text>();
        victoryScreen = this.transform.Find("VictoryScreen").gameObject;
        tutorialButton = this.transform.Find("TutorialButton").gameObject.GetComponent<Button>();
        print("Called");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseable && !Hidden)
        {

            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                UnHideEverything();
            }
            else
            {
                Time.timeScale = 0;
                HideEverything();
            }
            
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

        
	}
    public static void TransitionIn()
    {
        animator.SetTrigger("TransitionIn");
    }
    public static void SetBottomText(string text)
    {
        BottomUIText.text = text;
    }
    public static void SetTopText(string text)
    {
        TopUIText.text = text;
    }
    public static void UISetup(ProblemData data)
    {
        BottomUIText.text = data.Message;
        TransitionIn();
    }

    public void resumeGameOnClick()
    {
        UnHideEverything();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public static void ShowWrongAnswerText()
    {
        if (!showingWrongText)
        {
            showingWrongText = true;
            previousBottomText = BottomUIText.text;
            SetBottomText("Wrong Answer. Try Again!");
            animator.SetTrigger("ShowWrongAnswer");
        }
    }
    private void restorePreviousBottomText()
    {
        showingWrongText = false;
        SetBottomText(previousBottomText);
        TransitionIn();
    }
    public static string GetInputText()
    {
        return inputField.text;
    }
    public static void ShowInputHUD()
    {
        InputCanvas.SetActive(true);
    }
    public static void HideInputHUD()
    {
        InputCanvas.SetActive(false);
    }
    public static void SetupInputBox(string message,UnityAction action)
    {
        inputField.text = message;
        InputButton.onClick.RemoveAllListeners();
        InputButton.onClick.AddListener(action);
        ShowInputHUD();
    }
    public static void SetActionButtonEvent(UnityAction action)
    {
        ActionButton.onClick.RemoveAllListeners();
        ActionButton.onClick.AddListener(action);
    }
    public static void AppendToInfoBox(string text)
    {
        StringBuilder builder = new StringBuilder(InfoText.text);
        builder.AppendLine("\n" + text);
        InfoText.text = builder.ToString();
    }
    public static void ClearInfoBox()
    {
        InfoText.text = "InfoBox";
    }
    public static void ShowInfoBox()
    {
        infoBox.SetActive(true);
    }
    public static void HideInfoBox()
    {
        infoBox.SetActive(false);
    }
    public static void ShowVictoryScreen()
    {
        GameTimer.timerActive = false;
        pauseable = false;
        victoryScreen.SetActive(true);
        string victoryTime = "Time Completed : " + GameTimer.GetTimeString();
        victoryScreen.transform.FindChild("VictoryText").GetComponent<Text>().text = victoryTime;
        print(GameTimer.getTimeInSeconds());
    }
    public static void SetVictoryButtonEvent(string message, UnityAction action)
    {
        Button b = victoryScreen.transform.FindChild("GoToNextLevelButton").GetComponent<Button>();
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(action);
        victoryScreen.transform.FindChild("GoToNextLevelButton").transform.FindChild("Text").GetComponent<Text>().text = message;
    }
    public static void HideEverything()
    {
        print("Hiding HUD");
        tutorialButton.interactable = false;
        ActionButton.interactable = false;
        List<GameObject> tmp = new List<GameObject>();
        tmp.Add(TopUIText.gameObject);
        tmp.Add(BottomUIText.gameObject);
        tmp.Add(InputCanvas);
        GameObject grid = GameObject.Find("Grid");
        if (grid != null)
        {
            if (grid.activeSelf)
            {
                tmp.Add(grid);
            }
        }
        for(int i = 0; i < tmp.Count; i++)
        {
            if(tmp[i].activeSelf != false)
            {
                tmp[i].SetActive(!tmp[i].activeSelf);
                States.Add(tmp[i]);
            }
        }
    }
    public static void UnHideEverything()
    {
        tutorialButton.interactable = true;
        ActionButton.interactable = true;
        for (int i = 0; i < States.Count; i++)
        {
            States[i].SetActive(!States[i].activeSelf);
        }
        States.Clear();
        Hidden = false;
    }
    public static void SetTutorialButtonState(bool state)
    {
        tutorialButton.interactable = state;
    }
}

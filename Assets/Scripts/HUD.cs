using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class HUD : MonoBehaviour {
    static Text BottomUIText;
    static Text TopUIText;
    static Animator animator;
    private GOM_Script gom;
    private GameObject pauseMenu;
    private static string previousBottomText;
    private static bool showingWrongText = false;
    public static InputField inputField;
    static GameObject InputCanvas;
    static Button InputButton;
    static Button ActionButton;
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
        print("Called");
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (pauseMenu.active)
            {
               
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            pauseMenu.SetActive(!pauseMenu.active);
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
        BottomUIText.text = data.message;
        TransitionIn();
    }

    public void resumeGameOnClick()
    {
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
}

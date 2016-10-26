using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
    static Text BottomUIText;
    static Text TopUIText;
    static Animator animator;
    private GOM_Script gom;
    private GameObject pauseMenu;
	// Use this for initialization
	void Start () {
        BottomUIText = this.transform.Find("UIDisplayText").GetComponent<Text>();
        TopUIText = this.transform.Find("UITopText").GetComponent<Text>();
        animator = GetComponent<Animator>();
        pauseMenu = this.transform.Find("PauseMenu").gameObject;
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
        // BottomUIText.text = "Encrypt: " + gom.ceasarQuestionOne.plaintext +"with key: " + gom.ceasarQuestionOne.key;
        BottomUIText.text = data.message;
        TransitionIn();
    }

    public void resumeGameOnClick()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}

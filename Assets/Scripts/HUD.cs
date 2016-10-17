using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
    static Text BottomUIText;
    static Text TopUIText;
    static Animator animator;
    private GOM_Script gom;
	// Use this for initialization
	void Start () {
        BottomUIText = this.transform.Find("UIDisplayText").GetComponent<Text>();
        TopUIText = this.transform.Find("UITopText").GetComponent<Text>();
        animator = GetComponent<Animator>();
        print("Called");
	}
	
	// Update is called once per frame
	void Update () {
	
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
}

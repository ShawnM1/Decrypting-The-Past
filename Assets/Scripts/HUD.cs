using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
    static Text BottomUIText;
    static Animator animator;
    private GOM_Script gom;
	// Use this for initialization
	void Start () {
        BottomUIText = this.transform.Find("UIDisplayText").GetComponent<Text>();
        animator = GetComponent<Animator>();
        gom = FindObjectOfType<GOM_Script>();
        print("Called");
        UISetup();
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
    public void UISetup()
    {
        BottomUIText.text = "Encrypt: " + gom.ceasarQuestionOne.plaintext +"with key: " + gom.ceasarQuestionOne.key;
        TransitionIn();
    }
}

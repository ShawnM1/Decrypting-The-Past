using UnityEngine;
using System.Collections;

public class GOM_Script : MonoBehaviour {
    // public ProblemData[] problemDataArray;
    public ProblemData ceasarQuestionOne;
    public AnswerInput input;
    

	// Use this for initialization
	void Start () {
        ceasarQuestionOne = new ProblemData("1","hello", "ifmmp", TextType.CipherText);
     
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void CheckAnswer()
    {
        if(ceasarQuestionOne.compareResult(input.currentText))
        {
            print("Correct");
        }
        else
        {
            print("Hosed");
        }
    }
}

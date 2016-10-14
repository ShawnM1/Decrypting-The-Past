using UnityEngine;
using System.Collections;

public class GOM_Script : MonoBehaviour {
   // public ProblemData[] problemDataArray;
    public ProblemData ceasarQuestionOne = new ProblemData();
    public AnswerInput input;
    

	// Use this for initialization
	void Start () {
        ceasarQuestionOne = new ProblemData();
        ceasarQuestionOne.key = "1";
        ceasarQuestionOne.plaintext = "hello";
        ceasarQuestionOne.ciphertext = "ifmmp";
        ceasarQuestionOne.ProblemType = TextType.CipherText;

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

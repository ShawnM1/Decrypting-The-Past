using UnityEngine;
using System.Collections;

public class GOM_Script : MonoBehaviour {
    // public ProblemData[] problemDataArray;
    //public ProblemData ceasarQuestionOne;
    public ProblemHandler input;
    

	// Use this for initialization
	void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void CheckAnswer()
    {
        /*if(input.problems[0].compareResult(input.currentText))
        {
            print("Correct");
        }
        else
        {
            print("Hosed");
        }*/
        if(input.GoToNextProblem())
        {
            
        }
    }
}

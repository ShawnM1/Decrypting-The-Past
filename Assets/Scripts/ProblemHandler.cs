using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public abstract class ProblemHandler : MonoBehaviour {

    #region Variables
    public ProblemData[] problems = null;
    private string currentText = "";
    private int currentProblem = 0;
    #endregion
    #region Properties
    /// <summary>
    /// Current text is the text used by the HUD
    /// </summary>
    public string CurrentText
    {
        get
        {
            return currentText;
        }
    }
    /// <summary>
    /// Returns the currently active problem
    /// </summary>
    public ProblemData CurrentProblemData
    {
        get
        {
            return problems[currentProblem];
        }
    }
    #endregion
    public virtual void Start()
    {
        currentText = "ISKYIQ";
        ProblemSetup(CurrentProblemData);
        HUD.UISetup(problems[currentProblem]);
    }
    
    public void CickToGoToNextProblem()
    {
        GoToNextProblem();
    }
    /// <summary>
    /// Checks to see if the problem is solved. If so, move on to the next problem. If its the last problem solved, show stats and go to the next level.
    /// </summary>
    /// <returns></returns>
    public bool GoToNextProblem()
    {
        if (currentProblem <= problems.Length - 1)
        {
            if (problems[currentProblem].compareResult(currentText))
            {
                print("Correct");
                currentProblem++;
                currentText = "";
                //Do UI BS
                if(currentProblem <= problems.Length - 1)
                {
                    OnGoToNextProblem();
                    HUD.UISetup(problems[currentProblem]);
                    
                    return true;
                }
                else
                {
                    OnAllProblemsSolved();
                    return true;
                }
            }
            else
            {
                print("Wrong Answer .exe");
                return false;
            }
        }
        return false;
    }
    /// <summary>
    /// Goes to the next problem
    /// </summary>
    public virtual void OnGoToNextProblem()
    {
        ProblemSetup(CurrentProblemData);
    }
    
    /// <summary>
    /// Takes input text and appends it to our current text variable
    /// </summary>
    /// <param name="text">Input Text</param>
    public void AppendCurrentText(string text)
    {
        StringBuilder builder = new StringBuilder(currentText);
        builder.Append(text);
        currentText = builder.ToString();
        UpdateUI();
        print(currentText);
    }
    public virtual void Update()
    {
        //Handles deleting a character off the end of current text
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentText.Length > 0)
            {
                currentText = currentText.Remove(currentText.Length - 1, 1);
                UpdateUI();
            }
        }
    }
    #region Abstract Methods (These are defined in Cipher)
    /// <summary>
    /// Event for what happens when all problems have been solved.
    /// </summary>
    public abstract void OnAllProblemsSolved();
    /// <summary>
    /// Updates the UI Text after appending to it
    /// </summary>
    public abstract void UpdateUI();
    public abstract string GenerateCipherText();
    public abstract string GeneratePlainText();
    public abstract void ProblemSetup(ProblemData data);
    #endregion
}

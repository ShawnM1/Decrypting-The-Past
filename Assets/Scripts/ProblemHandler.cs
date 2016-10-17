using UnityEngine;
using System.Collections;
using System.Text;

public abstract class ProblemHandler : MonoBehaviour {
  
    public ProblemData[] problems = null;
    private string currentText = "";
    private int currentProblem = 0;
    public string CurrentText
    {
        get
        {
            return currentText;
        }
    }
    public virtual void Start()
    {
        HUD.UISetup(problems[currentProblem]);
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
    /// <summary>
    /// Checks to see if the problem is solved. If so, move on to the next problem. If its the last problem solved, show stats and go to the next level.
    /// </summary>
    /// <returns></returns>
    public bool GoToNextProblem()
    {
        if(problems[currentProblem].compareResult(currentText))
        {
            currentProblem++;
            currentText = "";
            //Ghetto Way of skipping the rest of the execution
            if(currentProblem > problems.Length - 1)
            {
                OnAllProblemsSolved();
                return true;
            }
            //Do UI BS
            HUD.UISetup(problems[currentProblem]);
            OnGoToNextProblem();
            return true;
        }
        else
        {
            print("Wrong Answer .exe");
            return false;
        }
    }
    /// <summary>
    /// Goes to the next problem
    /// </summary>
    public abstract void OnGoToNextProblem();
    /// <summary>
    /// Event for what happens when all problems have been solved.
    /// </summary>
    public abstract void OnAllProblemsSolved();
    /// <summary>
    /// Updates the UI Text after appending to it
    /// </summary>
    public abstract void UpdateUI();
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
    
}

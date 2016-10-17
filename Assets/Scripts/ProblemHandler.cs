using UnityEngine;
using System.Collections;
using System.Text;

public abstract class ProblemHandler : MonoBehaviour {


    public string currentText = "";
    public ProblemData[] problems = null;
    int currentProblem = 0;
    public virtual void Start()
    {
        HUD.UISetup(problems[currentProblem]);
    }
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentText.Length > 0)
            {
                currentText = currentText.Remove(currentText.Length - 1, 1);
                UpdateUI();
            }
        }
    }
    public bool GoToNextProblem()
    {
        if(problems[currentProblem].compareResult(currentText))
        {
            currentProblem++;
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
    public abstract void OnGoToNextProblem();
    public abstract void OnAllProblemsSolved();
    /// <summary>
    /// Updates the UI Text after appending to it
    /// </summary>
    public abstract void UpdateUI();
    public void AppendCurrentText(string text)
    {
        StringBuilder builder = new StringBuilder(currentText);
        builder.Append(text);
        currentText = builder.ToString();
        UpdateUI();
        print(currentText);
    }
    
}

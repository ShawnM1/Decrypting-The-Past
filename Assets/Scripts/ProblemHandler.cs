using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public abstract class ProblemHandler : MonoBehaviour {

    #region Variables
    public List<string> _words = new List<string>();
    public List<ProblemData> problems = new List<ProblemData>();
    private bool lockInput = false;
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
    public void PopulateWordDictionary(params string[] words)
    {
        foreach(string x in words)
        {
            _words.Add(x.ToLower());
        }
    }
    string GetRandomWord()
    {
        if(_words.Count > 0)
        {
            string tmp = _words[Random.Range(0, _words.Count)];
            _words.Remove(tmp);
            return tmp;
        }
        return null;
    }
    public void AddProblem(ProblemData data)
    {
        if(_words.Count > 0)
        {
            ProblemData tmp = new ProblemData(this, data.key, data.ProblemType);
            tmp.plaintext = GetRandomWord();
            problems.Add(tmp);
        }
    }
    public virtual void Start()
    {
        ProblemSetup(CurrentProblemData);
        HUD.UISetup(problems[currentProblem]);
    }
    
    public void CickToGoToNextProblem()
    {
        print("PlainText: " + CurrentProblemData.plaintext);
        print("CipherText: " + CurrentProblemData.ciphertext);
        GoToNextProblem();
    }
    /// <summary>
    /// Checks to see if the problem is solved. If so, move on to the next problem. If its the last problem solved, show stats and go to the next level.
    /// </summary>
    /// <returns></returns>
    public bool GoToNextProblem()
    {
        if (currentProblem <= problems.Count - 1)
        {
            if (problems[currentProblem].compareResult(currentText))
            {
                print("Correct");
                currentProblem++;
                currentText = "";
                //Do UI BS
                if(currentProblem <= problems.Count - 1)
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
                HUD.ShowWrongAnswerText();
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
        if (!lockInput)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (currentText.Length > 0)
                {
                    currentText = currentText.Remove(currentText.Length - 1, 1);
                    UpdateUI();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //GoToNextProblem();
            }
        }
    }
    public void UnlockInput()
    {
        lockInput = false;
    }
    public void LockInput()
    {
        lockInput = true;
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
    public virtual void ProblemSetup(ProblemData data)
    {
        CurrentProblemData.UpdateMessage();
        if (Application.isEditor)
        {
            if (CurrentProblemData.ProblemType == TextType.Encryption)
            {
                currentText = CurrentProblemData.ciphertext;
            }
            else
            {
                currentText = CurrentProblemData.plaintext;
            }
        }
        HUD.UISetup(CurrentProblemData);
    }
    #endregion
}

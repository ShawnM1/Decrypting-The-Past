﻿using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;


public delegate void OnProblemChangedEventHandler();
public abstract class ProblemHandler : MonoBehaviour {

    #region Variables
    public List<string> _words = new List<string>();
    public List<ProblemData> problems = new List<ProblemData>();
    private bool lockInput = false;
    private string currentText = "";//
    private int currentProblem = 0;
    bool debugMode = false;
    public event OnProblemChangedEventHandler handler;

    public GameObject SavePrefab;
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
    void Awake()
    {
        // Creates dummy save data if we start the scene from the editor.
#if DEBUG

        GameObject.Instantiate(SavePrefab);
#endif
    }
    public void PopulateWordDictionary(params string[] words)
    {
        foreach(string x in words)
        {
            _words.Add(x.ToLower());
        }
    }
    public string GetRandomWord()
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
            ProblemData tmp = new ProblemData(this, data.key, data.ProblemType, data._level);
            tmp.Plaintext = GetRandomWord();
            problems.Add(tmp);
        }
    }
    public virtual void Start()
    {
        ProblemSetup(CurrentProblemData);
        HUD.UISetup(problems[currentProblem]);
        FireOnProblemChangedEventHandler();
    }
    
    public void CickToGoToNextProblem()
    {
        GoToNextProblem();
    }
    public bool GoToNextProblem(string currentText)
    {
        this.currentText = currentText;
        return GoToNextProblem();
    }
    /// <summary>
    /// Checks to see if the problem is solved. If so, move on to the next problem. If its the last problem solved, show stats and go to the next level.
    /// </summary>
    /// <returns></returns>
    public bool GoToNextProblem()
    {
        print("Goto Next Problem Called");
        if (currentProblem <= problems.Count - 1)
        {
            if (problems[currentProblem].compareResult(currentText))
            {
                print("Correct");
                currentProblem++;
                currentText = "";
                if(currentProblem <= problems.Count - 1)
                {
                    OnGoToNextProblem();
                    HUD.UISetup(problems[currentProblem]);
                    return true;
                }
                else
                {
                    OnAllProblemsSolved();
                    HUD.ShowVictoryScreen();
                    return true;
                }
            }
            else
            {
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
        FireOnProblemChangedEventHandler();
    }
    
    /// <summary>
    /// Takes input text and appends it to our current text variable
    /// </summary>
    /// <param name="text">Input Text</param>
    public void AppendCurrentText(string text)
    {
        StringBuilder builder = new StringBuilder(this.currentText);
        builder.Append(text);
        this.currentText = builder.ToString();
        
        UpdateUI();
        print(this.currentText);
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
    public void CheckInputFromHUD()
    {
        GoToNextProblem();
    }
    void FireOnProblemChangedEventHandler()
    {
        if(handler != null)
        {
            handler();
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
    /// <summary>
    /// Executes anything that is needed to set up problems
    /// </summary>
    /// <param name="data"></param>
    public virtual void ProblemSetup(ProblemData data)
    {
        CurrentProblemData.UpdateMessage();
        if (Application.isEditor && debugMode)
        {
            if (CurrentProblemData.ProblemType == TextType.Encryption)
            {
                currentText = CurrentProblemData.Ciphertext;
            }
            else
            {
                currentText = CurrentProblemData.Plaintext;
            }
        }
        HUD.UISetup(CurrentProblemData);
    }
    #endregion
}

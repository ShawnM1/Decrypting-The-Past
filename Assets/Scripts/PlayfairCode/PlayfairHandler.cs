using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayfairHandler : ProblemHandler
{
    private string answer;
    private GameObject keyInput;
    private GameObject gameObjectMatrix;
    private GameObject player;
    private GameObject UserMatrixText;
    private int matrixCounter = 0;
    public PlayfairCipher cipher;
    void Start()
    {
        UserMatrixText = GameObject.Find("UserMatrixTextbox");
        LockInput();
        keyInput = GameObject.Find("KeyInputCanvas");
        gameObjectMatrix = GameObject.Find("Grid");
        player = GameObject.Find("CharacterRobotBoy");
        PopulateWordDictionary("juice", "diffie", "hellman", "hello", "secret", "bugatti","cicirello", "keyboard", "snoopdogg", "unity","jackhammer", "juniper");
        AddProblem(new ProblemData(this, GetRandomWord(), TextType.Encryption, CipherType.Playfair));
        AddProblem(new ProblemData(this, GetRandomWord(), TextType.Decryption, CipherType.Playfair));
        //PopulateWordDictionary("rob", "joe", "hello", "bugatti");
        //AddProblem(new ProblemData(this,"secret", TextType.Encryption));
        //AddProblem(new ProblemData(this, "clams", TextType.Decryption));
        base.Start();


    }
    /// <summary>
    /// Method to check if the user formatted the key correctly
    /// </summary>
    public void checkKeyInput()
    {
        if (HUD.GetInputText().ToUpper().Equals(cipher.formatKey(CurrentProblemData.key).ToUpper()))
        {
            //Correct Move to step 2
            if (CurrentProblemData.ProblemType == TextType.Encryption)
            {
                HUD.SetTopText("Please format the plaintext");
                HUD.SetupInputBox("Please Format the PlainText", checkPlainTextInput);
            }
            else
            {
                SetupMatrixInput();
            }
        }
        else
        {
            HUD.ShowWrongAnswerText();
        }
    }
    /// <summary>
    /// Method to check if user formatted plaintext correctly
    /// </summary>
    public void checkPlainTextInput()
    {
        if (HUD.GetInputText().ToUpper().Equals(cipher.formatPlaintext(CurrentProblemData.Plaintext).ToUpper()))
        {
            ///Next we fill the matrix
            ///Call the matrix UI
            SetupMatrixInput();
        }
        else
        {
            HUD.ShowWrongAnswerText();
        }
    }
    /// <summary>
    /// User enters matrix row by row, check each row to see if it is correct.
    /// </summary>
    public void checkMatrixInput()
    {
        GameObject UserMatrixText = GameObject.Find("UserMatrixTextbox");
        if (HUD.GetInputText().ToUpper().Equals(cipher.GetRow(matrixCounter).ToUpper()) && (matrixCounter < 5))
        {
            UserMatrixText.GetComponent<Text>().text += HUD.GetInputText().ToUpper() + '\n';
            matrixCounter++;
            HUD.SetupInputBox("Please enter the " + (matrixCounter) + " row of the matrix", checkMatrixInput);
            if (matrixCounter > 4)
            {
                //Fade the UI out and Display the matrix and play the game
                UserMatrixText.GetComponent<Text>().text = "";
                //GameObject.Destroy(UserMatrixText,0);
                ShowMatrix();
                matrixCounter = 0;
                HUD.HideInputHUD();
            }
        }
        else
        {
            HUD.ShowWrongAnswerText();
        }
    }
    void ShowMatrix()
    {
        UnlockInput();
        gameObjectMatrix.SetActive(true);
        player.SetActive(true);
        HUD.SetActionButtonEvent(CickToGoToNextProblem);
    }
    void SetupMatrixInput()
    {
        HUD.SetTopText("Please enter each row of the matrix");
        HUD.SetupInputBox("Please enter the 0 row of the Matrix", checkMatrixInput);
        matrixCounter = 0;
    }

    public override void ProblemSetup(ProblemData data)
    {
        cipher = new PlayfairCipher(data.key);
        //Call Grid here
        HUD.SetTopText("Please Format the Key");
        player.SetActive(false);
        LockInput();
        HUD.SetupInputBox("Please Format the Key", checkKeyInput);
        cipher.fillMatrix(data.key);
        data.Ciphertext = GenerateCipherText();
        CurrentProblemData.UpdateMessage();
        gameObjectMatrix.GetComponent<PlayfairGrid>().AppendLettersToObjectMatrix(cipher.getMatrix());
        gameObjectMatrix.SetActive(false);
        if (data.ProblemType == TextType.Encryption)
        {
            answer = cipher.encrypt(data.Plaintext);
        }
        else if (data.ProblemType == TextType.Decryption)
        {
            answer = cipher.decrypt(data.Ciphertext);
        }
        base.ProblemSetup(data);
    }
    public override void OnGoToNextProblem()
    {
        //Recreate Matrix
        base.OnGoToNextProblem();
    }

    public override void OnAllProblemsSolved()
    {
        SaveContainer.Instance.SaveFile.PlayfairCompleted = true;
        SaveContainer.Instance.SaveFile.PlayfairCompletionTime = (int)GameTimer.getTimeInSeconds();
        SaveContainer.Instance.SaveDataToFile();
    }

    public override void UpdateUI()
    {
        HUD.SetTopText(CurrentText);
    }

    public override string GenerateCipherText()
    {
        print("pt " + CurrentProblemData.Plaintext);
        return cipher.encrypt(CurrentProblemData.Plaintext);
    }

    public override string GeneratePlainText()
    {
        return "";
    }
}





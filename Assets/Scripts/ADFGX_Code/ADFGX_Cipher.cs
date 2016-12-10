using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;



class ADFGX_Cipher : ProblemHandler
{
    private StringBuilder matrixText;
    private static char[,] table;
    private static char[,] matrix = new char[5,5];
#pragma warning disable 
    public GameObject TransposeInput;
#pragma warning disable
    public GameObject ADFGXMatrix;

    
    void Start()
    {
        fillMatrix();
        PopulateWordDictionary("Car", "Hi", "Rob");
        AddProblem(new ProblemData(this, generateRandomKey(), TextType.Encryption, CipherType.ADFGX));
        AddProblem(new ProblemData(this, generateRandomKey(), TextType.Decryption, CipherType.ADFGX));
        AddProblem(new ProblemData(this, generateRandomKey(), TextType.Encryption, CipherType.ADFGX));
        GameObject.FindObjectOfType<ADFGXMatrix>().injectADFGX(matrix);
        HUD.ShowInfoBox();
        base.Start();
    }
    /// <summary>
    /// Generates a random permutation of a key length 4
    /// </summary>
    /// <returns>String</returns>
    private string generateRandomKey()
    {
        StringBuilder key = new StringBuilder();
        StringBuilder keyOptions = new StringBuilder("1234");
        System.Random r = new System.Random();
        while(keyOptions.Length > 0)
        {
            int randomPos = r.Next(0, keyOptions.Length - 1);
            key.Append(keyOptions[randomPos]);
            keyOptions.Remove(randomPos, 1);
        }
        return key.ToString();

    }
    /// <summary>
    /// Checks user input after clicking matrix elements on UI
    /// If correct, display Transpose UI.
    /// </summary>
    private void checkMatrixText()
    {
        if (CurrentProblemData.ProblemType == TextType.Encryption)
        {
            if (matrixText.ToString().Equals(CurrentText))
            {
                TransposeInput.SetActive(true);
                ADFGXMatrix.GetComponent<ADFGXMatrix>().DisableInput();
                this.LockInput();
            } else
            {
                HUD.ShowWrongAnswerText();
            }
        }
    }
    /// <summary>
    /// Returns the encoding of a plaintext character referencing the ADFGX matrix.
    /// </summary>
    /// <param name="c">plaintext character</param>
    /// <returns>String</returns>
    public string GetEncodedCharText(char c)
    {
        StringBuilder encodedCharText = new StringBuilder();
        int charRow = getCharRowIndex(c);
        int charCol = getCharColIndex(c);
        if (charCol != 0)
        {
            switch (charRow)
            {
                
                case 0:
                // Empty since this row of the matrix is the title ADFGX, and not actual alphabet letters
                case 1:
                    encodedCharText.Append('A');
                    break;
                case 2:
                    encodedCharText.Append('D');
                    break;
                case 3:
                    encodedCharText.Append('F');
                    break;
                case 4:
                    encodedCharText.Append('G');
                    break;
                case 5:
                    encodedCharText.Append('X');
                    break;
            }
            switch (charCol)
            {
                case 0:
                // Empty since this row of the matrix is the title ADFGX, and not actual alphabet letters
                case 1:
                    encodedCharText.Append('A');
                    break;
                case 2:
                    encodedCharText.Append('D');
                    break;
                case 3:
                    encodedCharText.Append('F');
                    break;
                case 4:
                    encodedCharText.Append('G');
                    break;
                case 5:
                    encodedCharText.Append('X');
                    break;
            }
        }
        return encodedCharText.ToString();
    }
    /// <summary>
    /// Encrypts plaintext. Note the all j's are replaced with i's
    /// </summary>
    /// <param name="plaintext"></param>
    /// <returns> cipher text</returns>
    private string encrypt(string plaintext)
    {
        plaintext = plaintext.ToLower().Replace('j', 'i');
        // Matrix text are the characters encoded by the ADFGX matrix
        matrixText = new StringBuilder();
        StringBuilder ciphertext = new StringBuilder();
        for (int i = 0; i < plaintext.Length; i++)
        {
            matrixText.Append(GetEncodedCharText(plaintext[i]));
        }
  
        fillTable(matrixText.ToString());
        // The following for loop accesses columns in numeric order by name. For example, in 
        // the key 231, the third row "1" is accessed first, followed by 2, then 3.
        for (int i = 0; i < base.CurrentProblemData.key.Length; i++)
        {
            // loop through 1-4 and find its index in the key
            string s = (i + 1).ToString();
            int index = base.CurrentProblemData.key.IndexOf(s);
            switch(base.CurrentProblemData.key.IndexOf(s))
            {
                case 0:
                    ciphertext.Append(getColumn(index));
                    break;
                case 1:
                    ciphertext.Append(getColumn(index));
                    break;
                case 2:
                    ciphertext.Append(getColumn(index));
                    break;
                case 3:
                    ciphertext.Append(getColumn(index));
                    break;

            }
        }

        return ciphertext.ToString().Replace(" ", string.Empty);
        

    }/// <summary>
    /// Normally decryption works by taking the ciphertext and putting it into the correct
    /// column positions/lengths in the key table to find the matrixText. Since we are always encrypting, we have the table
    /// and matrixText already. Simply take the matrixText, and use the letter indexes to reference the matrix.
    /// </summary>
    /// <returns></returns>
    public string decrypt()
    {
        StringBuilder _ciphertext = new StringBuilder();
       // match pair chars from matrixText to plaintext (in matrix)
       for (int i = 0; i < matrixText.Length; i+=2)
        {
            char c1 = matrixText[i];
            int indexChar1 = -1;

            char c2 = matrixText[i + 1];
            int indexChar2 = -1;

            switch(c1)
            {
                case 'A':
                    indexChar1 = 0;
                    break;
                case 'D':
                    indexChar1 = 1;
                    break;
                case 'F':
                    indexChar1 = 2;
                    break;
                case 'G':
                    indexChar1 = 3;
                    break;
                    
                case 'X':
                    indexChar1 = 4;
                    break;                  
             }
            switch(c2)
            {
                case 'A':
                    indexChar2 = 0;
                    break;
                case 'D':
                    indexChar2 = 1;
                    break;
                case 'F':
                    indexChar2 = 2;
                    break;
                case 'G':
                    indexChar2 = 3;
                    break;
                case 'X':
                    indexChar2 = 4;
                    break;
            }
            _ciphertext.Append(matrix[indexChar1, indexChar2]);

        }
        return _ciphertext.ToString();

    }
    /// <summary>
    /// Organizes matrix text according to the key order. i.e. 231
    /// where the third column labeled "1" will be accessed first inside
    /// the encrypt function
    /// </summary>
    /// <param name="matrixText">passed in from encrypt method</param>
    private void fillTable(string matrixText)
    {
        StringBuilder text = new StringBuilder(matrixText);
        // should always be 4 since key is always length 4
        int numOfCols = base.CurrentProblemData.key.Length;
        int numOfRows;
        // if there are left over characters, we will need an extra row
        if (text.Length % numOfCols != 0)
        {
            numOfRows = (text.Length / numOfCols) + 1;
        }
        else
        {
            numOfRows = text.Length / numOfCols;
        }
        table = new char[numOfRows, numOfCols];

        for (int i = 0; i < numOfRows; i++)
        {
            for (int k = 0; k < numOfCols; k++)
            {
                // Add the first character to the matrix and remove it from the string. Repeat until string is empty.
                if (text.Length > 0)
                {
                    char addedChar = text[0];

                    table[i, k] = addedChar;
                    text.Remove(0, 1);
                }
                else
                {
                    table[i, k] = ' ';
                }

            }

        }

        printMatrix(table);
    }
    /// <summary>
    /// Accesses the matrix on screen to compare and locate the character's row in the matrix
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>int coressponding to the row of the matrix</returns>
    private static int getCharRowIndex(char c)
    {
        char[,] array = GameObject.Find("Grid").GetComponent<ADFGXMatrix>().ADFGX_array;
        int row = -1;
        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 6; k++)
            {
                char temp;
                temp = array[i, k];
                if (c.Equals(temp))
                {
                    row = i;
                    break;
                }
            }
        }
        return row;

    }
    /// <summary>
    ///  Accesses the matrix on screen to compare and locate the character's column in the matrix
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>int coressponding to the column of the matrix</returns>
    private static int getCharColIndex(char c)
    {
        char[,] array = GameObject.Find("Grid").GetComponent<ADFGXMatrix>().ADFGX_array;
        
        int col = -1;
        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 6; k++)
            {
                char temp = array[i, k];
                if (c.Equals(temp))
                {
                    col = k;
                    break;
                }
            }
        }
        return col;
    }
    /// <summary>
    /// Used for debugging purposes
    /// </summary>
    /// <param name="matrix"></param>
    private static void printMatrix(char[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);
        for (int i = 0; i < rowLength; i++)
        {
            if (i > 0)
            {
                Console.WriteLine();
            }
            for (int k = 0; k < colLength; k++)
            {
                Console.Write(matrix[i, k]);
            }
        }
    }
    /// <summary>
    /// Gets the data in the specified column of the table consisting of matrix text.
    /// Used in the encrypt method
    /// </summary>
    /// <param name="index">int</param>
    /// <returns>string</returns>
    private string getColumn(int index)
    {   
        StringBuilder columnData = new StringBuilder();
        int rowLength = table.GetLength(0);
        int colLength = table.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int k = 0; k < colLength; k++)
            {
                if (k == index)
                {
                    columnData.Append(table[i, k]);
                }

            }
        }
        return columnData.ToString().Trim();
    }
    /// <summary>
    /// Randomly fills the matrix with text from the alphabet.
    /// All letters are unique. 
    /// </summary>
    private static void fillMatrix()
    {
        System.Random r = new System.Random();
        StringBuilder alphabet = new StringBuilder("abcdefghiklmnopqrstuvwxyz");

        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                int removeIndex = r.Next(0, alphabet.Length);
                matrix[i, k] = alphabet[removeIndex];
                alphabet.Remove(removeIndex, 1);
            }
        }
    }
    /// <summary>
    /// Loops through all 4 inputfield objects.
    /// Compares each text entry to the corresponding column in 
    /// table[,].
    /// </summary>
    /// <returns></returns>
    private bool checkTableInput()
    {

        bool b = false;
        for (int i = 0; i < 4; i++)
         {
            string input = GameObject.Find("InputField"+(i+1)).transform.FindChild("Text").GetComponent<Text>().text;
            char currentChar = base.CurrentProblemData.key[i];
            // index of c in the key
            int indexPos = base.CurrentProblemData.key.IndexOf(currentChar);
            string currentColumn = getColumn(indexPos);
            // Error logic
            if (!input.ToUpper().Equals(currentColumn))
             {
                 HUD.ShowWrongAnswerText();
                 b = false;
                 break;
             } else
             {
                 b = true;
             }
         }
       
        return b;

    }
  
    /// <summary>
    /// Method to check inputfield data in TransposeUI
    /// </summary>
    public void clickCheckTableInput()
    {
        if(checkTableInput())
        {
            if (CurrentProblemData.ProblemType == TextType.Encryption)
            {
                HUD.SetupInputBox("Please enter Ciphertext:", CheckUserCipherText);
            }
            else
            {
                HUD.SetupInputBox("Please enter Matrixtext:", CheckUserMatrixText);

            }
        }
    }
    public void CheckUserCipherText()
    {
        
        if(GoToNextProblem(HUD.GetInputText()))
        {
            HUD.HideInputHUD();
        }
    }
    void checkPlainTextFromMatrix()
    {
        GoToNextProblem();
    }
    /// <summary>
    /// During decryption, checks user matrix text after transpose step.
    /// If correct, make the matrix clickable.
    /// </summary>
    public void CheckUserMatrixText()
    {
        if(matrixText.ToString().ToUpper().Equals(HUD.GetInputText().ToUpper()))
        {
            HUD.AppendToInfoBox(matrixText.ToString().ToUpper());
            HUD.HideInputHUD();
            ADFGXMatrix.GetComponent<ADFGXMatrix>().EnableInput();
            this.UnlockInput();
            HUD.SetActionButtonEvent(checkPlainTextFromMatrix);
        }
        else
        {
            HUD.ShowWrongAnswerText();
        }
    }
    public override void OnAllProblemsSolved()
    {
        SaveContainer.Instance.SaveFile.ADFGXCompleted = true;
        SaveContainer.Instance.SaveFile.ADFGXCompletionTime = (int)GameTimer.getTimeInSeconds();
        SaveContainer.Instance.SaveDataToFile();
        HUD.SetVictoryButtonEvent("Go To Credits", GoToCredits);
    }
    void GoToCredits()
    {
        GoToScene.goToScene("CreditsScreen");
    }

    public override void UpdateUI()
    {
        HUD.SetTopText(CurrentText);
    }

    public override string GenerateCipherText()
    {
        return encrypt(base.CurrentProblemData.Plaintext);
    }

    public override string GeneratePlainText()
    {
        return "";
    }
    public override void ProblemSetup(ProblemData data)
    {
        fillMatrix();
        data.Ciphertext = GenerateCipherText();
        UpdateUI();
        HUD.ClearInfoBox();
        base.ProblemSetup(data);
        if(CurrentProblemData.ProblemType == TextType.Encryption)
        {
            HUD.SetActionButtonEvent(this.checkMatrixText);
            ADFGXMatrix.GetComponent<ADFGXMatrix>().EnableInput();
            TransposeInput.SetActive(false);
            HUD.HideInputHUD();
        }
        else
        {
            HUD.HideInputHUD();
            TransposeInput.SetActive(true);
            TransposeInput.GetComponent<TransposeOrdering>().UpdateUI();
            TransposeInput.GetComponent<TransposeOrdering>().ClearFields();
            ADFGXMatrix.GetComponent<ADFGXMatrix>().DisableInput();
        }   

    }

    #region Debug Functions
    public void printMatrixText()
    {
        print(matrixText);
    }
    public void printMatrix()
    {
        for(int i = 0; i < 5; i++)
        {
            if (i > 0)
            {
                print("\n");
            }
            for (int k = 0; k < 5; k++)
            {
                print(matrix[i, k].ToString()); 
            }
        }
    }
    #endregion

}


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Playfair : ProblemHandler
{
    static char[,] matrix = new char[5, 5];
    private static string keyword;
    private string plaintext;
    private string ciphertext;
    private static StringBuilder alphabet = new StringBuilder("abcdefghijklmnopqrstuvwxyz");
    private string answer;
    private GameObject keyInput;
    private int matrixCounter = 0;
    private GameObject gameObjectMatrix;
    private GameObject player;
    private GameObject UserMatrixText;


    void Start()
    {
        UserMatrixText = GameObject.Find("UserMatrixTextbox");
        LockInput();
        keyInput = GameObject.Find("KeyInputCanvas");
        gameObjectMatrix = GameObject.Find("Grid");
        player = GameObject.Find("CharacterRobotBoy");
        //"hello", "sponge", "cicirello", "jackhammer", "stockton", "rob", "secret", "project", "diffie");
        PopulateWordDictionary( "secret", "rob", "hello", "diffie");
        AddProblem(new ProblemData(this, GetRandomWord(), TextType.Encryption));
        AddProblem(new ProblemData(this, GetRandomWord(), TextType.Decryption));
       // AddProblem(new ProblemData(this, GetRandomWord(), TextType.Decryption));
       // AddProblem(new ProblemData(this, GetRandomWord(), TextType.Encryption));
        base.Start();
       

    }
    /// <summary>
    /// Method to check if the user formatted the key correctly
    /// </summary>
    public void checkKeyInput()
    {
        if(HUD.GetInputText().ToUpper().Equals(formatKey(CurrentProblemData.key).ToUpper()))
        {
            //Correct Move to step 2
            if(CurrentProblemData.ProblemType == TextType.Encryption)
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
        if(HUD.GetInputText().ToUpper().Equals(formatPlaintext(CurrentProblemData.Plaintext).ToUpper()))
        {
            ///Next we fill the matrix
            ///Call the matrix UI
            SetupMatrixInput();
        } else
        {
            HUD.SetTopText("Incorrect plaintext formatting");
        }
    }
    /// <summary>
    /// Helper method to get the specified row in the matrix.
    /// See use in checkMatrixInput() method.
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public string GetRow(int row)
    {
        StringBuilder builder = new StringBuilder();
        for(int i = 0; i < 5; i++)
        {
            builder.Append(matrix[row, i]);
        }
        return builder.ToString();
    }
    /// <summary>
    /// User enters matrix row by row, check each row to see if it is correct.
    /// </summary>
    public void checkMatrixInput()
    {
        GameObject UserMatrixText = GameObject.Find("UserMatrixTextbox");
        if (HUD.GetInputText().ToUpper().Equals(GetRow(matrixCounter).ToUpper()) && (matrixCounter < 5))
        {
            print(GetRow(matrixCounter));
            UserMatrixText.GetComponent<Text>().text += HUD.GetInputText().ToUpper() + '\n';
            matrixCounter++;
            HUD.SetupInputBox("Please enter the " + (matrixCounter) + " row of the matrix", checkMatrixInput);
            if(matrixCounter > 4)
            {
                //Fade the UI out and Display the matrix and play the game
                UserMatrixText.GetComponent<Text>().text = "";
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
        keyword = data.key;
        //Call Grid here
        HUD.SetTopText("Please Format the Key");
        player.SetActive(false);
        LockInput();
        HUD.SetupInputBox("Please Format the Key", checkKeyInput);
        print(data.key);
        fillMatrix(keyword);
        data.Ciphertext = GenerateCipherText();
        CurrentProblemData.UpdateMessage();
        gameObjectMatrix.GetComponent<PlayfairGrid>().AppendLettersToObjectMatrix(getMatrix());
        gameObjectMatrix.SetActive(false);
        ciphertext = data.Ciphertext;
        plaintext = data.Plaintext;
        if (data.ProblemType == TextType.Encryption)
        {
            answer = encrypt(plaintext);
        }
        else if (data.ProblemType == TextType.Decryption)
        {
            answer = decrypt(ciphertext);
        }
        base.ProblemSetup(data);
    }

    /// <summary>
    /// Method to remove non-distinct characters.
    /// Each char of the string is compared to the
    /// other characters. remove if comparison is the same
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static String formatKey(String key)
    {
        key = key.ToLower();
        key = key.Replace("j", "i");
        StringBuilder formattedKey = new StringBuilder(key);

        char currentChar;
        char compareChar;

        for (int i = 0; i < formattedKey.Length; i++)
        {
            currentChar = formattedKey[i];

            if (i == formattedKey.Length - 1)
            {
                break;
            }
            else
            {
                for (int k = i + 1; k < formattedKey.Length; k++)
                {

                    compareChar = formattedKey[k];
                    if (compareChar.Equals(currentChar))
                    {

                        formattedKey.Remove(k, 1);
                    }

                }
            }


        }
 
        return formattedKey.ToString();
    }
    /// <summary>
    /// Fill the matrix with the formatted key word.
    /// Proceed to fill with the rest of the alphabet.
    /// </summary>
    /// <param name="key"></param>
    private static void fillMatrix(String key)
    {
        alphabet = new StringBuilder("abcdefghijklmnopqrstuvwxyz");
        string fKey = formatKey(key);
        print("fk" + fKey);
        StringBuilder formattedKey = new StringBuilder(fKey);
        int leaveOfPos = 0;
        // Number of characters that will spill over to the next row
        int remainderCharacters = formattedKey.Length % 5;
        // Number of complete rows filled by the keyword
        int rowsForKey = formattedKey.Length / 5;
        
        // We need to access an additional row if we have extra characters
        if (remainderCharacters != 0 && fKey.Length > 5)
        {
            rowsForKey += 1;
        }

        for (int i = 0; i <= rowsForKey; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                if (formattedKey.Length > 0)
                {
                    // Add the keyword character to the matrix and remove itself
                    // remove the added letter from out alphabet string using helper method
                    char addedChar = formattedKey[0];
                    int alphabetCharIndex = characterIndex(alphabet, addedChar);
                    alphabet.Remove(alphabetCharIndex, 1);

                    matrix[i, k] = addedChar;
                    formattedKey.Remove(0, 1);
                    // Console.WriteLine(formattedKey);
                    // reference value to fill in the rest of the matric with the alphabet

                }
                else
                {
                    leaveOfPos = k; 
                    break;
                }
            }
        }
        // I and J must be mutually exclusive. Format accordingly
        int indexOf_I = characterIndex(alphabet, 'i');
        int indexOf_J = characterIndex(alphabet, 'j');

        for (int i = 0; i < alphabet.Length; i++)
        {
            // Both in alphabet, choose one to delete (j)
            if (indexOf_I > 0 && indexOf_J > 0)
            {
                alphabet.Remove(indexOf_J, 1);
                break;
            }
            // I is in the alphabet and J is not (meaning J is in the matrix)
            else if (indexOf_I > 0 && indexOf_J == -1)
            {
                alphabet.Remove(indexOf_I, 1);
                break;
            }
            // J is in the alphabet and I is not (meaning I is in the matrix)
            else if (indexOf_J > 0 && indexOf_I == -1)
            {
                alphabet.Remove(indexOf_J, 1);
                break;
            }
        }
 
        // First lets fill the complete row

        int startRow = fKey.Length / 5;
        int remainingChars = fKey.Length % 5;
        int emptyElements = 5 - remainingChars;

        if (fKey.Length % 5 != 0)
        {

            for (int i = leaveOfPos; i < 5; i++)
            {
                matrix[startRow, leaveOfPos] = alphabet[0];
                alphabet.Remove(0, 1);

            }
            // Now we can fill the rest of the complete rows
            for (int i = startRow + 1; i < 5 - startRow; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    matrix[i, k] = alphabet[0];
                    alphabet.Remove(0, 1);
                }
            }
        }   
        else if (fKey.Length % 5 == 0)
        {
            for (int i = startRow; i < 5 - startRow + 1; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    matrix[i, k] = alphabet[0];
                    alphabet.Remove(0, 1);
                }
            }
        }

    }
    /// <summary>
    /// Returns the index of the char in the specified string.
    /// Used in formatKey and fillMatrix
    /// </summary>
    /// <param name="c"></param>
    private static int characterIndex(StringBuilder s, char c)
    {
        int index = -1;
        for (int i = 0; i < s.Length; i++)
        {
            char currentChar = s[i];
            if (currentChar.Equals(c))
            {
                index = i;
                break;
            }
        }
        return index;
    }
 
    /// <summary>
    /// No pair of letters can be the same. Put an x
    /// inbetween them. E.g. hello --> helxlo
    /// Also there must be an even amount of chars.
    /// Append an X char is there is an odd amount.
    /// </summary>
    /// <param name="plaintext"></param>
    private static string formatPlaintext(string plaintext)
    {
        plaintext = plaintext.Replace(" ", string.Empty).ToLower();
        plaintext = plaintext.Replace("j", "i");
        StringBuilder formattedPlaintext = new StringBuilder();

        for (int i = 0; i < plaintext.Length; i += 2)
        {

            // logic to avoid out of bounds error since we are iterating by 2.
            // See respective else to append last char
            if (i < plaintext.Length - 1)
            {
                char currnetChar = plaintext[i];
                char neighborChar = plaintext[i + 1];

                if (currnetChar.Equals(neighborChar))
                {
                    // They are the same. Before appending, check to see that the last
                    // character in the stringbuilder is not the same as the one we are appending


                    if (formattedPlaintext.Length > 0)
                    {
                        // Case where last char in stringbuilder is the same as the one we are appending.
                        char lastChar = formattedPlaintext[formattedPlaintext.Length - 1];
                        if (lastChar.Equals(currnetChar))
                        {
                            formattedPlaintext.Append('x');
                            formattedPlaintext.Append(currnetChar);
                            formattedPlaintext.Append('x');
                            formattedPlaintext.Append(neighborChar);
                        }
                        // Case where last char in stringbuilder is not the same as the one we are appending
                        else
                        {
                            formattedPlaintext.Append(currnetChar);
                            formattedPlaintext.Append('x');
                            formattedPlaintext.Append(neighborChar);
                        }


                    }
                    // StringBuilder empty, only ran on first iteration
                    else
                    {
                        formattedPlaintext.Append(currnetChar);
                        formattedPlaintext.Append('x');
                        formattedPlaintext.Append(neighborChar);

                    }

                }
                // currentChar and neighborChar are different
                else
                {
                    formattedPlaintext.Append(currnetChar);
                    formattedPlaintext.Append(neighborChar);
                }

            }
            // Left over character from +2 increment
            else
            {
                char lastPlaintextChar = plaintext[plaintext.Length - 1];
                char lastFormattedPlaintextChar = formattedPlaintext[formattedPlaintext.Length - 1];

                if (lastPlaintextChar.Equals(lastFormattedPlaintextChar))
                {
                    formattedPlaintext.Append('x');
                    formattedPlaintext.Append(lastPlaintextChar);
                }
                else
                {
                    formattedPlaintext.Append(lastPlaintextChar);
                }
            }


        }
        // Need an event amount of characters to have pairs. Append x
        if (formattedPlaintext.Length % 2 != 0)
        {
            formattedPlaintext.Append('x');
        }
        return formattedPlaintext.ToString();

    }
    /// <summary>
    /// Encrypt plaintext using the playfair encryption rules
    /// </summary>
    /// <param name="plaintext"></param>
    /// <returns>Ciphertext from the plaintext</returns>
    private static string encrypt(String plaintext)
    {
        char[,] m = getMatrix();
        StringBuilder formattedPlaintext = new StringBuilder(formatPlaintext(plaintext));
        print(formattedPlaintext);
        StringBuilder ciphertext = new StringBuilder();

        for (int i = 0; i < formattedPlaintext.Length; i += 2)
        {
            char c1 = formattedPlaintext[i];
            int c1Row = charRowPosition(c1);
            int c1Column = charColumnPosition(c1);

            char c2 = formattedPlaintext[i + 1];
            int c2Row = charRowPosition(c2);
            int c2Column = charColumnPosition(c2);

            // Case: chars are in same row of the matrix
            if (c1Row == c2Row)
            {
                // If at the end of the row, wrap around
                if (c1Column == 4)
                {
                    ciphertext.Append(matrix[c1Row, 0]);
                }
                else
                {
                    ciphertext.Append(matrix[c1Row, c1Column + 1]);
                }

                if (c2Column == 4)
                {
                    ciphertext.Append(matrix[c2Row, 0]);
                }
                else
                {
                    ciphertext.Append(matrix[c2Row, c2Column + 1]);
                }
            }
            // Case: chars are in the same column in the matrix. 
            else if (c1Column == c2Column)
            {
                // If at the end of the column, wrap around
                if (c1Row == 4)
                {
                    ciphertext.Append(matrix[0, c1Column]);
                }
                else
                {
                    ciphertext.Append(matrix[c1Row + 1, c1Column]);
                }
                if (c2Row == 4)
                {
                    ciphertext.Append(matrix[0, c2Column]);
                }
                else
                {
                    ciphertext.Append(matrix[c2Row + 1, c2Column]);
                }
            }
            // Case where pair of characters are not in same row or column
            else
            {
                ciphertext.Append(matrix[c1Row, c2Column]);
                ciphertext.Append(matrix[c2Row, c1Column]);
            }
        }
        return ciphertext.ToString();

    }/// <summary>
    /// Decrypts ciphertext into original plaintext message
    /// </summary>
    /// <param name="ciphertext">String ciphertext</param>
    /// <returns>String that is the plaintext message</returns>
    private static string decrypt(String ciphertext)
    {
        StringBuilder formattedPlaintext = new StringBuilder(formatPlaintext(ciphertext));
        StringBuilder originalPlaintext = new StringBuilder();

        for (int i = 0; i < formattedPlaintext.Length; i += 2)
        {
            char c1 = formattedPlaintext[i];
            int c1Row = charRowPosition(c1);
            int c1Column = charColumnPosition(c1);

            char c2 = formattedPlaintext[i + 1];
            int c2Row = charRowPosition(c2);
            int c2Column = charColumnPosition(c2);

            // Case: chars are in same row of the matrix
            if (c1Row == c2Row)
            {
                // If at the end of the row, wrap around
                if (c1Column == 4)
                {
                    originalPlaintext.Append(matrix[c1Row, 0]);
                }
                else
                {
                    originalPlaintext.Append(matrix[c1Row, c1Column - 1]);
                }

                if (c2Column == 4)
                {
                    originalPlaintext.Append(matrix[c2Row, 0]);
                }
                else
                {
                    originalPlaintext.Append(matrix[c2Row, c2Column - 1]);
                }
            }
            // Case: chars are in the same column in the matrix. 
            else if (c1Column == c2Column)
            {
                // If at the end of the column, wrap around
                if (c1Row == 4)
                {
                    originalPlaintext.Append(matrix[0, c1Column]);
                }
                else
                {
                    originalPlaintext.Append(matrix[c1Row - 1, c1Column]);
                }
                if (c2Row == 4)
                {
                    originalPlaintext.Append(matrix[0, c2Column]);
                }
                else
                {
                    originalPlaintext.Append(matrix[c2Row - 1, c2Column]);
                }
            }
            // Case where pair of characters are not in same row or column
            else
            {
                originalPlaintext.Append(matrix[c1Row, c2Column]);
                originalPlaintext.Append(matrix[c2Row, c1Column]);
            }
        }
        return originalPlaintext.ToString();

    }

    /// <summary>
    /// Reurns the row index of specified char in matrix
    /// </summary>
    /// <param name="c">char c</param>
    /// <returns>int row index of the character</returns>
    private static int charRowPosition(char c)
    {
        int index = -1;
        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                if (matrix[i, k].Equals(c))
                {
                    index = i;
                    break;
                }
            }
        }
        return index;
    }
    /// <summary>
    /// Returns the column index of the specified char in matrix
    /// </summary>
    /// <param name="c">char c</param>
    /// <returns>int column index of the character</returns>
    private static int charColumnPosition(char c)
    {
        int index = -1;
        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                if (matrix[i, k].Equals(c))
                {
                    index = k;
                    break;
                }
            }
        }
        return index;
    }
    private static char[,] getMatrix()
    {
        return matrix;
    }
    public override void OnGoToNextProblem()
    {
        //Recreate Matrix
        base.OnGoToNextProblem();
    }

    public override void OnAllProblemsSolved()
    {
        SaveContainer.Instance.SaveFile.CaesarCompleted = true;
        SaveContainer.Instance.SaveFile.CaesarCompletionTime = (int)GameTimer.getTimeInSeconds();
        SaveContainer.Instance.SaveDataToFile();
    }

    public override void UpdateUI()
    {
        HUD.SetTopText(CurrentText);
    }

    public override string GenerateCipherText()
    {
        print(CurrentProblemData.key);
        return encrypt(CurrentProblemData.Plaintext);
    }

    public override string GeneratePlainText()
    {
        return "";
    }
}





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;



class ADFGX_Cipher : ProblemHandler
{
    private StringBuilder matrixText;
    public static char[,] table;
    public static char[,] matrix = new char[5,5];


    void Start()
    {
        fillMatrix();
       // printMatrix();
        PopulateWordDictionary("Car", "Hello");
        AddProblem(new ProblemData(this, generateRandomKey(), TextType.Encryption));
        AddProblem(new ProblemData(this, generateRandomKey(), TextType.Decryption));
        GameObject.FindObjectOfType<PlayfairGrid>().injectADFGX(matrix);
        base.Start();
       
      //  print("ct " + this.GenerateCipherText());
    }

    public string generateRandomKey()
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
    public void checkMatrixText()
    {
        if (CurrentProblemData.ProblemType == TextType.Encryption)
        {
            print(this.CurrentText);
            if (this.CurrentText.Equals(matrixText))
            {
                print("Matrix Text Correct");
                //Show Table With cool effect
            }
            else
            {
                print("Incorrect Matrix Text");
            }
        }
        else
        {

        }
    }
    public string getEncodedCharText(char c)
    {
        StringBuilder encodedCharText = new StringBuilder();
        int charRow = getCharRowIndex(c);
        int charCol = getCharColIndex(c);
        // breakpoint
        if (charCol != 0)
        {
            switch (charRow)
            {
                case 0:

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
        // breakpoint
       // print(c + "              " + "Row :   " + charRow + " Col :  " + charCol + " converts to " + encodedCharText);
        // breakpoint
        // print(encodedCharText);
        return encodedCharText.ToString();

    }
    public string encrypt(string plaintext)
    {

        matrixText = new StringBuilder();
        StringBuilder ciphertext = new StringBuilder();
        char currentChar;
        int currentCharRow;
        int currentCharCol;
        for (int i = 0; i < plaintext.Length; i++)
        {
            matrixText.Append(getEncodedCharText(plaintext[i]));
           
            /*
            currentChar = plaintext[i];
            currentCharRow = getCharRowIndex(currentChar);
            currentCharCol = getCharColIndex(currentChar);

            switch (currentCharRow)
            {
                case 0:
                    matrixText.Append('A');
                    break;
                case 1:
                    matrixText.Append('D');
                    break;
                case 2:
                    matrixText.Append('F');
                    break;
                case 3:
                    matrixText.Append('G');
                    break;
                case 4:
                    matrixText.Append('X');
                    break;
            }
            switch (currentCharCol)
            {
                case 0:
                    matrixText.Append('A');
                    break;
                case 1:
                    matrixText.Append('D');
                    break;
                case 2:
                    matrixText.Append('F');
                    break;
                case 3:
                    matrixText.Append('G');
                    break;
                case 4:
                    matrixText.Append('X');
                    break;
            }
            */
        }
        fillTable(matrixText.ToString());

        for (int i = 0; i < base.CurrentProblemData.key.Length; i++)
        {
           
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
        // TODO: remove whitespace
        // return removeWhitespace(ciphertext.ToString());
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
    public void fillTable(string matrixText)
    {
        StringBuilder text = new StringBuilder(matrixText);

        int numOfCols = base.CurrentProblemData.key.Length;
        int numOfRows;
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
    private static int getCharRowIndex(char c)
    {
        char[,] array = GameObject.Find("Grid").GetComponent<PlayfairGrid>().ADFGX_array;
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
    private static int getCharColIndex(char c)
    {
        char[,] array = GameObject.Find("Grid").GetComponent<PlayfairGrid>().ADFGX_array;
        
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
    private static string removeWhitespace(string s)
    {
      
        for (int i = 0; i < s.Length; i++)
        {
            
            if (s[i].Equals(' '))
            {
                Console.WriteLine("True");
                s.Remove(i, 1);
            }
        }
        return s;

    }
    private static void fillMatrix()
    {
        System.Random r = new System.Random();
        StringBuilder alphabet = new StringBuilder("abcdefghiklmnopqrstuvwxyz");

        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                int removeIndex = r.Next(0, alphabet.Length);
                //print(removeIndex + " " + alphabet);
                matrix[i, k] = alphabet[removeIndex];
                alphabet.Remove(removeIndex, 1);
            }
        }
    }
    /// <summary>
    /// loops through all 4 inputfield objects.
    /// Compares each text entry to the corresponding column in 
    /// table[,].
    /// </summary>
    /// <returns></returns>
    public bool checkTableInput()
    {

        bool b = false;
        for (int i = 0; i < 4; i++)
         {
            string input = GameObject.Find("InputField"+(i+1)).transform.FindChild("Text").GetComponent<Text>().text;
            char currentChar = base.CurrentProblemData.key[i];
            // index of c in the key
            int indexPos = base.CurrentProblemData.key.IndexOf(currentChar);
            string currentColumn = getColumn(indexPos);
            
            if (!input.ToUpper().Equals(currentColumn))
             {
                 print("inputfield" + (i+1) + " is incorrect");
                
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
    /// Method to check inputfield data in panel.
    /// </summary>
    public void clickCheckTableInput()
    {
        if(checkTableInput())
        {
            print("Table Correct");
        }
    }
    

    public override void OnAllProblemsSolved()
    {
        print("winner winner chicken dinner");
    }

    public override void UpdateUI()
    {
        HUD.SetTopText(CurrentText);
    }

    public override string GenerateCipherText()
    {
        return encrypt(base.CurrentProblemData.plaintext);
    }

    public override string GeneratePlainText()
    {
        return "";
    }

    public override void ProblemSetup(ProblemData data)
    {
        fillMatrix();
        data.ciphertext = GenerateCipherText();
        base.ProblemSetup(data);
        if(CurrentProblemData.ProblemType == TextType.Encryption)
        {
            //MapUI Button
            HUD.SetActionButtonEvent(() => this.checkMatrixText());
        }   

    }
    

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

}


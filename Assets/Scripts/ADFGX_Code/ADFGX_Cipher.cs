using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



class ADFGX_Cipher : ProblemHandler
{
    private string key;
    private string plaintext;
    private string ciphertext;
    private StringBuilder matrixText;
    public static char[,] table;
    public static char[,] matrix = new char[5,5];


    void start()
    {
        fillMatrix();
        printMatrix();
        PopulateWordDictionary("Car", "Hello");
        AddProblem(new ProblemData(this, "312", TextType.Encryption));
        AddProblem(new ProblemData(this, "231", TextType.Decryption));
        
    }


    /*public ADFGX_Cipher(string key, string ciphertext, string plaintext)
    {
        this.key = key;
        this.plaintext = plaintext;
        this.ciphertext = ciphertext;

    
    }*/

    public string encrypt(string plaintext)
    {

        matrixText = new StringBuilder();
        StringBuilder ciphertext = new StringBuilder();
        char currentChar;
        int currentCharRow;
        int currentCharCol;
        for (int i = 0; i < plaintext.Length; i++)
        {
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

        }
        fillTable(matrixText.ToString());

        for (int i = 0; i < key.Length; i++)
        {
           
            string s = (i+1).ToString();
            int index = key.IndexOf(s);
            switch(key.IndexOf(s))
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

        int numOfCols = key.Length;

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
        int row = -1;
        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                char temp;
                temp = matrix[i, k];
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
        int col = -1;
        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                char temp = matrix[i, k];
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
    private static string getColumn(int index)
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
        return columnData.ToString();
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
        int alphabetLength = alphabet.Length - 1;

        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                int removeIndex = r.Next(0, alphabetLength);
                matrix[i, k] = alphabet[removeIndex];
                alphabet.Remove(removeIndex, 1);
            }
        }
    }

    public override void OnAllProblemsSolved()
    {
        print("winner winner chicken dinner");
    }

    public override void UpdateUI()
    {
        
    }

    public override string GenerateCipherText()
    {
        return encrypt(CurrentProblemData.plaintext);
    }

    public override string GeneratePlainText()
    {
        return "";
    }

    public override void ProblemSetup(ProblemData data)
    {
        fillMatrix();


    }
    public void printTesting()
    {
        print(encrypt("hello"));
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
                print(matrix[i, k]);
            }
        }
    }
}


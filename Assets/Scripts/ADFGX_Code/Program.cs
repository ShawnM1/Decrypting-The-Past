using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



class ADFGX_Cipher
{
    private string key;
    private string plaintext;
    private string ciphertext;
    private static char[,] table;
    static char[,] matrix;


    static void Main(string[] args)
    {
        ADFGX_Cipher obj = new ADFGX_Cipher("312", "placeholder", "hello");
        string ciphertext = obj.encrypt();
        Console.WriteLine("");
        Console.WriteLine(ciphertext);
        
        Console.ReadLine();
    }
    public ADFGX_Cipher(string key, string ciphertext, string plaintext)
    {
        this.key = key;
        this.plaintext = plaintext;
        this.ciphertext = ciphertext;
        matrix = new char[5, 5]{ 
        
                /*a*/  /*d*/ /*f*/ /*g*/ /*x*/
       /*a*/     { 'b', 't', 'a', 'l', 'p'}, 
       /*d*/     { 'd', 'h', 'o', 'z', 'k'}, 
       /*f*/     { 'q', 'f', 'v', 's', 'n'}, 
       /*g*/     { 'g', 'i', 'c', 'u', 'x'},
       /*x*/     { 'm', 'r', 'e', 'w', 'y'}};

    }
    public string encrypt()
    {
        string plaintext = this.plaintext;
        StringBuilder matrixText = new StringBuilder();
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
                char temp = matrix[i, k];
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

}


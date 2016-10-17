using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Playfair : ProblemHandler
{
    static char[,] matrix = new char[5, 5];
    private static string keyword;
    private string plaintext;
    private string ciphertext;
    private static StringBuilder alphabet = new StringBuilder("abcdefghijklmnopqrstuvwxyz");
    private string answer;
    

    void Start()
    {
        /*Console.WriteLine("Encrypt (E) or Decrypt (D)");
        string input = Console.ReadLine().ToString().ToLower();
        Console.WriteLine("enter a keyword");

        keyword = Console.ReadLine();
        fillMatrix(keyword);


        if (input.Equals("e"))
        {

            Console.WriteLine("enter the plaintext to encrypt");
            string plaintext = Console.ReadLine().ToString();
            Console.WriteLine(encrypt(plaintext));


        } else if (input.Equals("d"))
        {
            Console.WriteLine("enter ciphertext to decrypt");



        }
        Console.ReadLine();*/
        problems = new ProblemData[1];
        problems[0] = new ProblemData("secret", "hello", "iskyiq", TextType.CipherText);
        ProblemSetup(problems[0]);
        base.Start();
        

    }
    void ProblemSetup(ProblemData data)
    {
        keyword = data.key;
        fillMatrix(keyword);
        //Call Grid here
        GetComponent<PlayfairGrid>().AppendLettersToObjectMatrix(getMatrix());
        ciphertext = data.ciphertext;
        plaintext = data.plaintext;
        if (data.ProblemType == TextType.CipherText)
        {
            answer = encrypt(plaintext);
        }
        else if (data.ProblemType == TextType.PlainText)
        {
            answer = decrypt(ciphertext);
        }
    }

    /// <summary>
    /// Method to remove non-distance characters.
    /// Each char of the string is compared to the
    /// other characters. remove if comparison is the same
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private static String formatKey(String key)
    {
        key.ToLower();
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
        // TODO: I and J must be mutually exclusive in the matrix
        int indexOf_I = characterIndex(formattedKey, 'i');
        int indexOf_J = characterIndex(formattedKey, 'j');

        if (indexOf_I > 0 && indexOf_J > 0)
        {
            formattedKey.Remove(indexOf_J, 1);
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
        String fKey = formatKey(key);

        StringBuilder formattedKey = new StringBuilder(fKey);

        // Number of characters that will spill over to the next row
        int remainderCharacters = formattedKey.Length % 5;
        // Number of complete rows filled by the keyword.
        int rowsForKey = formattedKey.Length / 5;
        // We need to access an additional row if we have extra characters
        if (remainderCharacters != 0)
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
        // TODO: fill the matrix with the remaining alphabet
        // First lets fill the complete row

        int startRow = fKey.Length / 5;
        int remainingChars = fKey.Length % 5;
        int emptyElements = 5 - remainingChars;

        if (fKey.Length % 5 != 0)
        {


            for (int i = remainingChars; i <= emptyElements; i++)
            {
                matrix[startRow, i] = alphabet[0];
                alphabet.Remove(0, 1);

            }
            // Now we can fill the rest of the complete rows
            for (int i = startRow + 1; i < 5 - startRow + 1; i++)
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
    private static void printMatrx()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine();
            for (int k = 0; k < 5; k++)
            {
                Console.Write(matrix[i, k]);
            }
        }

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
        plaintext.Replace(" ", string.Empty).ToLower();
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
        StringBuilder formattedPlaintext = new StringBuilder(formatPlaintext(plaintext));
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

    }
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
    /// <param name="c"></param>
    /// <returns></returns>
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
    /// <param name="c"></param>
    /// <returns></returns>
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
    }

    public override void OnAllProblemsSolved()
    {
        //Nex tLEvel
    }

    public override void UpdateUI()
    {
        HUD.SetTopText(CurrentText);
    }
}





using UnityEngine;
using System.Collections;
using System.Text;
using System;

public class CaesarCipher {

    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    /// <summary>
    /// Returns the alphabet string
    /// </summary>
    /// <returns></returns>
    public string GetAlphabet()
    {
        return alphabet;
    }
    /// <summary>
    /// This function can encrypt or decrypt a cipher depending wheather the key is positive.
    /// See decrypt method.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public string encryptCipher(int key, string message)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < message.Length; i++)
        {
            int localKey = key;
            if(localKey < 0)
            {
                localKey = findLetterPositionInAlpha(message[i]) + (key + 26);
            }
            else
            {
                localKey = (findLetterPositionInAlpha(message[i]) + key);
            }
            builder.Append(alphabet[localKey % 26]);
        }
        return builder.ToString();
    }
    /// <summary>
    /// Calls encryptCipher with negative key value.
    /// </summary>
    /// <param name="key">integer that is negated</param>
    /// <param name="message">plaintext essage</param>
    /// <returns>string ciphertext</returns>
    public string decrypt(int key, string message)
    {
        return encryptCipher(key, message);
    }
    private int findLetterPositionInAlpha(char c)
    {
        for (int i = 0; i < alphabet.Length; i++)
        {
            if (c == alphabet[i])
            {
                System.Diagnostics.Debug.WriteLine(c + "i pos " + i);
                return i;
            }
        }
        return -1;
    }
}

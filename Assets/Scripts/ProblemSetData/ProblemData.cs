using UnityEngine;
using System.Collections;
 public enum TextType
{
    Encryption,
    Decryption
}

public class ProblemData : ScriptableObject {
    // Data
    
    public string key;
   
    public string plaintext;
    public string ciphertext;
    public TextType ProblemType;
    public string message;
    ProblemHandler handler;
    string DisplayText;

    public ProblemData(ProblemHandler _handler,string key,TextType ProblemType)
    {
        this.key = key;
        this.plaintext = plaintext;
        this.ciphertext = "";

        this.ProblemType = ProblemType;
        handler = _handler;
 
    }
    public void UpdateMessage()
    {
        switch (ProblemType)
        {
            case TextType.Encryption:
                {
                    ciphertext = handler.GenerateCipherText();
                    message = "Encrpyt " + plaintext + " with key of " + key;
                    break;
                }
            case TextType.Decryption:
                {
                    ciphertext = handler.GenerateCipherText();
                    message = "Decrpyt " + ciphertext + " with key of " + key;
                    break;
                }
            default:
                {
                    message = "error";
                    break;
                }
        }
    }
    public bool compareResult(string answer)
    {
        answer = answer.ToLower();
        switch (ProblemType)
        {
            case TextType.Decryption:
                {
                    return answer.Equals(plaintext.ToLower());

                }
            case TextType.Encryption:
                {
                    return answer.Equals(ciphertext.ToLower());

                }
            default:
                {
                    return false;
                }

        }
    }
}

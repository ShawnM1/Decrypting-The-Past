using UnityEngine;
using System.Collections;
 public enum TextType
{
    Encryption,
    Decryption
}

public class ProblemData : ScriptableObject {

    public string key;
    public string Plaintext;
    public string Ciphertext;
    public TextType ProblemType;
    public string Message;
    ProblemHandler handler;
    string displayText;

    public ProblemData(ProblemHandler _handler,string key,TextType ProblemType)
    {
        this.key = key;
        this.Ciphertext = "";
        
        this.ProblemType = ProblemType;
        handler = _handler;
 
    }
    /// <summary>
    /// Updates the problem message for the UI to show.
    /// </summary>
    public void UpdateMessage()
    {
        switch (ProblemType)
        {
            case TextType.Encryption:
                {
                    Ciphertext = handler.GenerateCipherText();
                    Message = "Encrpyt " + Plaintext + " with key of " + key;
                    break;
                }
            case TextType.Decryption:
                {
                    Ciphertext = handler.GenerateCipherText();
                    Message = "Decrpyt " + Ciphertext + " with key of " + key;
                    break;
                }
            default:
                {
                    Message = "error";
                    break;
                }
        }
    }
    /// <summary>
    /// Checks the final result from the answer input
    /// </summary>
    /// <param name="answer">Wills be ciphertext or plaintext</param>
    /// <returns>True if answer matches</returns>
    public bool compareResult(string answer)
    {
        answer = answer.ToLower();
        switch (ProblemType)
        {
            case TextType.Decryption:
                {
                    return answer.Equals(Plaintext.ToLower());

                }
            case TextType.Encryption:
                {
                    return answer.Equals(Ciphertext.ToLower());

                }
            default:
                {
                    return false;
                }

        }
    }
}

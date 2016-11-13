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

using UnityEngine;
using System.Collections;
 public enum TextType
{
    CipherText,
    PlainText
}

public class ProblemData : ScriptableObject {
    // Data
    
    public string key;
   
    public string plaintext;
    
    public string ciphertext;
    public TextType ProblemType;
    public string message;
    ProblemHandler handler;

    public ProblemData(ProblemHandler _handler,string key, string plaintext,TextType ProblemType)
    {
        this.key = key;
        this.plaintext = plaintext;
        this.ciphertext = "";
        this.ProblemType = ProblemType;
        handler = _handler;
        switch (ProblemType)
        {
            case TextType.CipherText:
                {
                    message = "Encrpyt " + plaintext + " with key of " + key;
                    break;
                }
            case TextType.PlainText:
                {
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
    public void UpdateMessage()
    {
        switch (ProblemType)
        {
            case TextType.CipherText:
                {
                    message = "Encrpyt " + plaintext + " with key of " + key;
                    break;
                }
            case TextType.PlainText:
                {
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
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public bool compareResult(string answer)
    {
        answer = answer.ToLower();
        switch (ProblemType)
        {
            case TextType.PlainText:
                {
                    return answer.Equals(plaintext.ToLower());

                }
            case TextType.CipherText:
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

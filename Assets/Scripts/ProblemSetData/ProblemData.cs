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

    public ProblemData(string key, string plaintext, string ciphertext, TextType ProblemType)
    {
        this.key = key;
        this.plaintext = plaintext;
        this.ciphertext = ciphertext;
        this.ProblemType = ProblemType;
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

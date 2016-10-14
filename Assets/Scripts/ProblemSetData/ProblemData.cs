using UnityEngine;
using System.Collections;
 public enum TextType
{
    CipherText,
    PlainText
}

public class ProblemData : MonoBehaviour {
    // Data
    
    public string key;
   
    public string plaintext;
    
    public string ciphertext;
    public TextType ProblemType;
     
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

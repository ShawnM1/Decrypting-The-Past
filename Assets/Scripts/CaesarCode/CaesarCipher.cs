using UnityEngine;
using System.Collections;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class CaesarCipher : ProblemHandler {
    Circle2DPointGenerator pointGenerator;
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    int anglePerItem;
    public TextMesh mesh;
    public TextMesh resultMesh;
    
	// Use this for initialization
	public override void Start () {
        PopulateWordDictionary("Hello", "Cicirello","Xana","Slack","Gamer");
        AddProblem(new ProblemData(this,"1", TextType.Encryption));
        AddProblem(new ProblemData(this,"2", TextType.Decryption));
        AddProblem(new ProblemData(this, "3", TextType.Encryption));
        AddProblem(new ProblemData(this, "4", TextType.Decryption));
        AddProblem(new ProblemData(this, "5", TextType.Encryption));

        pointGenerator = GetComponent<Circle2DPointGenerator>();
        anglePerItem = 360 / 25;
        mesh = this.transform.parent.Find("LetterBlock").GetComponent<TextMesh>();
        resultMesh = this.transform.parent.Find("StoredLetters").GetComponent<TextMesh>();
        print("Start Called");
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        float h = Input.GetAxis("Horizontal");
        float z = transform.localEulerAngles.z; ;
        z += 1 * h;
        transform.eulerAngles = new Vector3(0, 0, z);
        mesh.text = GetLetter().ToString();
        base.Update();
    }
    public char GetLetter()
    {
        return alphabet[((int)transform.localEulerAngles.z / anglePerItem)];
    }
    public override void UpdateUI()
    {
        resultMesh.text = CurrentText;
    }

    public override void OnGoToNextProblem()
    {
        UpdateUI();
        base.OnGoToNextProblem();
    }

    public override void OnAllProblemsSolved()
    {
        //Display Score window "YOU WIN"
        SceneManager.LoadScene("PlayFair");
    }

    public override string GenerateCipherText()
    {
        return EncryptCipher(int.Parse(CurrentProblemData.key), CurrentProblemData.plaintext);
    }

    public override string GeneratePlainText()
    {
        return EncryptCipher(-int.Parse(CurrentProblemData.key), CurrentProblemData.plaintext);
        throw new NotImplementedException();
    }

    public override void ProblemSetup(ProblemData data)
    {
        base.ProblemSetup(data);
    }
    /// <summary>
    /// This function can encrypt or decrypt a cipher depending wheather the key is positive.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public string EncryptCipher(int key, string message)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < message.Length; i++)
        {
            builder.Append(alphabet[(FindLetterPositionInAlpha(message[i]) + key) % 25]);
        }
        return builder.ToString();
    }
    private int FindLetterPositionInAlpha(char c)
    {
        for (int i = 0; i < alphabet.Length; i++)
        {
            if (c == alphabet[i])
            {
                return i;
            }
        }
        return -1;
    }
}

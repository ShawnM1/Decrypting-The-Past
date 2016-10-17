using UnityEngine;
using System.Collections;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class CircleRotatoPotato : ProblemHandler {
    Hosed hosed;
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    int anglePerItem;
    public TextMesh mesh;
    public TextMesh resultMesh;
    
	// Use this for initialization
	public override void Start () {
        problems = new ProblemData[2];
        problems[0] = new ProblemData("1", "hello", "ifmmp", TextType.CipherText);
        problems[1] = new ProblemData("2", "hello", "jgnnq", TextType.CipherText);
        hosed = GetComponent<Hosed>();
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
    }

    public override void OnAllProblemsSolved()
    {
        //Display Score window "YOU WIN"
        SceneManager.LoadScene("PlayFair");
    }
}

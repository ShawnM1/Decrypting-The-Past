using UnityEngine;
using System.Collections;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class CaesarCipherHandler : ProblemHandler {
    CaesarCipher cipher = new CaesarCipher();
    Circle2DPointGenerator pointGenerator;
    private int anglePerItem;
    public TextMesh Mesh;
    public TextMesh ResultMesh;
	// Use this for initialization
	public override void Start () {
        PopulateWordDictionary("Hello", "Cicirello","Xana","Slack","Gamer");
        System.Random r = new System.Random();
        AddProblem(new ProblemData(this,r.Next(1,6).ToString(), TextType.Decryption,CipherType.Caesar));
        AddProblem(new ProblemData(this, r.Next(1, 6).ToString(), TextType.Encryption, CipherType.Caesar));
        
        //AddProblem(new ProblemData(this, r.Next(1, 6).ToString(), TextType.Encryption));
        //AddProblem(new ProblemData(this, r.Next(1, 6).ToString(), TextType.Encryption));

        HUD.SetActionButtonEvent(CickToGoToNextProblem);
        pointGenerator = GetComponent<Circle2DPointGenerator>();
        anglePerItem = 360 / 25;
        Mesh = this.transform.parent.Find("LetterBlock").GetComponent<TextMesh>();
        ResultMesh = this.transform.parent.Find("StoredLetters").GetComponent<TextMesh>();
        base.Start();
        print("Level" + CurrentProblemData._level);
    }
	
	// Update is called once per frame
    /// <summary>
    /// Gets the angle of the wheel and updates the text accordingly
    /// </summary>
	public override void Update ()
    {
        float h = Input.GetAxis("Horizontal");
        float z = transform.localEulerAngles.z; ;
        z += 1 * h;
        transform.eulerAngles = new Vector3(0, 0, z);
        Mesh.text = GetLetter().ToString();
        base.Update();
    }
    /// <summary>
    /// Finds letter based on angle of wheel.
    /// </summary>
    /// <returns></returns>
    public char GetLetter()
    {
        return cipher.GetAlphabet()[((int)transform.localEulerAngles.z / anglePerItem)];
    }
    public override void UpdateUI()
    {
        ResultMesh.text = CurrentText;
    }

    public override void OnGoToNextProblem()
    {
        UpdateUI();
        base.OnGoToNextProblem();
    }

    public override void OnAllProblemsSolved()
    {
        SaveContainer.Instance.SaveFile.CaesarCompleted = true;
        SaveContainer.Instance.SaveFile.CaesarCompletionTime = (int)GameTimer.getTimeInSeconds();
        SaveContainer.Instance.SaveDataToFile();
    }

    void goToNextLevel()
    {
        StartCoroutine(GoToScene.GoToSceneEnumerator("TimeLineMenuScene"));
    }

    public override string GenerateCipherText()
    {
        return cipher.encryptCipher(int.Parse(CurrentProblemData.key), CurrentProblemData.Plaintext);
    }

    public override string GeneratePlainText()
    {
        return cipher.decrypt(-int.Parse(CurrentProblemData.key), CurrentProblemData.Plaintext);
    }

    public override void ProblemSetup(ProblemData data)
    {
        base.ProblemSetup(data);
    }
}

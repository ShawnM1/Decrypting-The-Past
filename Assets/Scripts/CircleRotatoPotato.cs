using UnityEngine;
using System.Collections;
using System.Text;

public class CircleRotatoPotato : AnswerInput {
    Hosed hosed;
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    int anglePerItem;
    public TextMesh mesh;
    public TextMesh resultMesh;
    
	// Use this for initialization
	void Start () {
        hosed = GetComponent<Hosed>();
        anglePerItem = 360 / 25;
        mesh = this.transform.parent.Find("LetterBlock").GetComponent<TextMesh>();
        resultMesh = this.transform.parent.Find("StoredLetters").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float z = transform.localEulerAngles.z; ;
        z += 1 * h;
        transform.eulerAngles = new Vector3(0, 0, z);
        mesh.text = GetLetter().ToString();
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentText.Length > 0)
            {
                currentText = currentText.Remove(currentText.Length - 1, 1);
                resultMesh.text = currentText;
            }
            
        }
    }
    public char GetLetter()
    {
        //print(((int)transform.localEulerAngles.z / anglePerItem) % 360);
        return alphabet[((int)transform.localEulerAngles.z / anglePerItem)];
    }
    public void AppendLetter()
    {
        StringBuilder builder = new StringBuilder(base.currentText);
        builder.Append(GetLetter().ToString());
        base.currentText = builder.ToString();
        resultMesh.text = base.currentText;
        print(base.currentText);
    }

}

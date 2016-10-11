using UnityEngine;
using System.Collections;
using System.Text;

public class CircleRotatoPotato : MonoBehaviour {
    Hosed hosed;
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    int anglePerItem;
    public TextMesh mesh;
    public TextMesh resultMesh;

    public string CurrentString = "";
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
            if (CurrentString.Length > 0)
            {
                CurrentString = CurrentString.Remove(CurrentString.Length - 1, 1);
                resultMesh.text = CurrentString;
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
        StringBuilder builder = new StringBuilder(CurrentString);
        builder.Append(GetLetter().ToString());
        CurrentString = builder.ToString();
        resultMesh.text = CurrentString;
        print(CurrentString);
    }
}

using UnityEngine;
using System.Collections;
using System.Text;

public class CircleRotatoPotato : MonoBehaviour {
    Hosed hosed;
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    int anglePerItem;
    public TextMesh mesh;

    public string CurrentString = "";
	// Use this for initialization
	void Start () {
        hosed = GetComponent<Hosed>();
        anglePerItem = 360 / 25;
        mesh = this.transform.parent.Find("LetterBlock").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float z = transform.localEulerAngles.z; ;
        z += 1 * h;
        transform.eulerAngles = new Vector3(0, 0, z);
        mesh.text = GetLetter().ToString();
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
        print(CurrentString);
    }
}

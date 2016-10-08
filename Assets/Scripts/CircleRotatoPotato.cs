using UnityEngine;
using System.Collections;

public class CircleRotatoPotato : MonoBehaviour {

    Hosed hosed;
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    int anglePerItem;
    public GameObject LetterPrefab;
    private EdgeCollider2D collider;
	// Use this for initialization
	void Start () {
        hosed = GetComponent<Hosed>();
        collider = GetComponent<EdgeCollider2D>();
        anglePerItem = 360 / hosed.NumPoints;
        GenerateLetters();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float z = transform.localEulerAngles.z; ;
        z += 1 * h;
        transform.eulerAngles = new Vector3(0, 0, z);
        print(GetLetter());
    }
    public char GetLetter()
    {
        print(((int)transform.localEulerAngles.z / anglePerItem) % 360);
        return alphabet[((int)transform.localEulerAngles.z / anglePerItem)];
    }
    public void GenerateLetters()
    {
        for(int i = 0; i < alphabet.Length; i++)
        {
            GameObject tmp = (GameObject)GameObject.Instantiate(LetterPrefab, new Vector3(collider.points[i].x, collider.points[i].y,this.transform.position.z),Quaternion.identity);
            tmp.transform.parent = this.transform.parent;
            tmp.GetComponent<TextMesh>().text = alphabet[i].ToString();

        }
    }
}

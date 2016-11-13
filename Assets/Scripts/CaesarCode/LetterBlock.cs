using UnityEngine;
using System.Collections;

public class LetterBlock : MonoBehaviour {

    private CaesarCipher caesarCipher;
	// Use this for initialization
	void Start () {
        caesarCipher = transform.parent.FindChild("HamsterWheel").GetComponent<CaesarCipher>();
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        caesarCipher.AppendCurrentText(caesarCipher.GetLetter().ToString());

    }
}

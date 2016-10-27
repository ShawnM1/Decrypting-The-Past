﻿using UnityEngine;
using System.Collections;

public class LetterBlock : MonoBehaviour {

    public CaesarCipher potato;
	// Use this for initialization
	void Start () {
        potato = transform.parent.FindChild("HamsterWheel").GetComponent<CaesarCipher>();
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        potato.AppendCurrentText(potato.GetLetter().ToString());

    }
}

using UnityEngine;
using System.Collections;

public class MoviePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void playMovie()
    {
        // this line of code will make the Movie Texture begin playing
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        GetComponent<AudioSource>().Play();

    }
}

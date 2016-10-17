using UnityEngine;
using System.Collections;

public class PlayfairButton : MonoBehaviour {

    public int DetectionRadius = 5;
    GameObject player;
    TextMesh mesh;
    Playfair playfair;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        mesh = GetComponent<TextMesh>();
        playfair = GameObject.FindObjectOfType<Playfair>();
	}
	
    void FixedUpdate()
    {
        if(Vector2.Distance(player.transform.position,this.transform.position) < 5)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                playfair.AppendCurrentText(mesh.text);
            }
        }
    }
}

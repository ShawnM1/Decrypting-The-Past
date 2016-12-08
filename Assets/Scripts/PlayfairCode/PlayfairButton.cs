using UnityEngine;
using System.Collections;

public class PlayfairButton : MonoBehaviour {

    public int DetectionRadius = 5;
    GameObject player;
    TextMesh mesh;
    ProblemHandler handler;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        mesh = GetComponent<TextMesh>();
        handler = GameObject.FindObjectOfType<ProblemHandler>();
	}
	
    void FixedUpdate()
    {
        if (player.activeSelf)
        {
            // If player is within range, they can interact with the button
            if (Vector2.Distance(player.transform.position, this.transform.position) < 5)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    handler.AppendCurrentText(mesh.text);
                }
            }
        }
    }
}

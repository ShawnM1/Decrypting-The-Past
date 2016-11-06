using UnityEngine;
using System.Collections;

public class ADFGXButton : MonoBehaviour
{

    public int DetectionRadius = 5;
    GameObject player;
    TextMesh mesh;
    ADFGX_Cipher handler;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mesh = GetComponent<TextMesh>();
        handler = GameObject.FindObjectOfType<ADFGX_Cipher>();
    }

    void FixedUpdate()
    {
        if (player.active)
        {
            if (Vector2.Distance(player.transform.position, this.transform.position) < 5)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    handler.AppendCurrentText(handler.getEncodedCharText(mesh.text[0]));
                }
            }
        }
    }
}
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
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

    void Update()
    {
        if ((Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), this.transform.position) < 5) && Input.GetMouseButtonDown(0))
        {
            handler.AppendCurrentText(handler.getEncodedCharText(mesh.text[0]));
        }
        
    }
}
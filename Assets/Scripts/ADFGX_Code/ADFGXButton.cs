using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class ADFGXButton : MonoBehaviour
{

    public int DetectionRadius = 5;
    TextMesh mesh;
    ADFGX_Cipher handler;
    private bool listenToInput = true;
    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<TextMesh>();
        handler = GameObject.FindObjectOfType<ADFGX_Cipher>();
    }

    void Update()
    {
        // If the player is within range of the button, they can interact with it.
        if ((Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), this.transform.position) < 5) && Input.GetMouseButtonDown(0) && listenToInput)
        {
            if (handler.CurrentProblemData.ProblemType == TextType.Encryption)
            {
                handler.AppendCurrentText(handler.GetEncodedCharText(mesh.text[0]));
            }
            else
            {
                handler.AppendCurrentText(mesh.text[0].ToString());
            }
        }
        
    }
    /// <summary>
    /// Sets whether the button is listening to input or not
    /// </summary>
    /// <param name="state"></param>
    public void SetListenState(bool state)
    {
        listenToInput = state;
    }
}
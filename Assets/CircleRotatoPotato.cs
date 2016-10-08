using UnityEngine;
using System.Collections;

public class CircleRotatoPotato : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        ////Quaternion theRotation = transform.localRotation;
        //theRotation.z += .1f*h;
        float x = transform.rotation.eulerAngles.z;
        x += .6f * h;
        transform.rotation = Quaternion.Euler(0, 0, x);
    }
}

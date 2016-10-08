using UnityEngine;
using System.Collections;

public class CircleRotatoPotato : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        Quaternion theRotation = transform.localRotation;
        theRotation.z += .1f*h;
        transform.rotation = theRotation;
    }
}

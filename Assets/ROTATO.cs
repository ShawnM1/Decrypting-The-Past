using UnityEngine;
using System.Collections;

public class ROTATO : MonoBehaviour
{//Functions and a fuck ton of properties. METRIC.
    // START WAKE UPDATE | FIXED UPDATE | DISPOSE
	// Use this for initialization
	
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.right *Time.deltaTime);

        // ...also rotate around the World's Y axis
        //transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
    }

}

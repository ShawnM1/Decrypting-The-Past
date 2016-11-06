using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransposeOrdering : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ProblemHandler ProblemHandlerObj = GameObject.FindObjectOfType<ProblemHandler>();

	    for (int i = 0; i < 4; i++)
        {

            transform.FindChild(i + "keyindex").GetComponent<Text>().text = ProblemHandlerObj.CurrentProblemData.key[i].ToString();
            print("Finding Objects");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransposeOrdering : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ProblemHandler ProblemHandlerObj = this.gameObject.GetComponent<ADFGX_Cipher>();
        print("KEY: " + ProblemHandlerObj.CurrentProblemData.key);
	    for (int i = 0; i < 4; i++)
        {
            print(i + "keyindex" + "Assigned to: " + ProblemHandlerObj.CurrentProblemData.key[i].ToString());
            GameObject.Find(i + "keyindex").GetComponent<Text>().text = ProblemHandlerObj.CurrentProblemData.key[i].ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class SaveContainerCreator : MonoBehaviour {

    // Use this for initialization
    public GameObject SaveContianerPrefab;
	void Start () {
	    if(!GameObject.FindGameObjectWithTag("Data"))
        {
            Instantiate(SaveContianerPrefab).name = "SaveContainerObject";
        }
	}
}

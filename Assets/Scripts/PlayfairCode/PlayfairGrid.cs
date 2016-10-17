using UnityEngine;
using System.Collections;

public class PlayfairGrid : MonoBehaviour {
    public GameObject prefab;
    public int xOffset = 1;
    public int width = 5;
    public int height = 5;

    private GameObject[,] objectMatrix = new GameObject[5, 5];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Awake()
    {
        for (int i = 0; i < width; i++)
        {
            for(int k = 0; k < height; k++)
            {
                GameObject objectElement = (GameObject)Instantiate(prefab);
                objectElement.transform.position = new Vector3(objectElement.transform.position.x + (i*xOffset), objectElement.transform.position.y + (k*xOffset), objectElement.transform.position.z);
                objectElement.transform.parent = this.transform;
                objectMatrix[i, k] = objectElement;

            }
        }
        this.transform.Rotate(0, 0, -90);
    }
    public void AppendLettersToObjectMatrix(char[,] array)
    {
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < height; k++)
            {
                objectMatrix[i, k].GetComponent<TextMesh>().text = array[i, k].ToString();
            }
        }
    }
}

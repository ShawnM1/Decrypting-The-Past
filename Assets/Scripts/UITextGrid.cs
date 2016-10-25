using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITextGrid : MonoBehaviour {

    public int width = 5;
    public int height = 5;
    public int xOffset = 1;
    private GameObject[,] objectMatrix;
	// Use this for initialization
	void Start () {
        objectMatrix = new GameObject[width, height];
        
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < height; k++)
            {

                GameObject objectElement = new GameObject();
                Text tmp = objectElement.AddComponent<Text>();
                tmp.rectTransform.sizeDelta = new Vector2(10, 10);
                objectElement = (GameObject)GameObject.Instantiate(objectElement,this.transform.position,Quaternion.identity);
                objectElement.transform.position = new Vector3(objectElement.transform.position.x + (i * xOffset), objectElement.transform.position.y + (k * xOffset), objectElement.transform.position.z);
                objectElement.transform.parent = this.transform;
                objectMatrix[i, k] = objectElement;

            }
        }

    }
}

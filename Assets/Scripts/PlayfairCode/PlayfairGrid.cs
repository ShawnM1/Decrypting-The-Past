using UnityEngine;
using System.Collections;

public class PlayfairGrid : MonoBehaviour {
    public GameObject Prefab;
    public int XOffset = 1;
    public int Width = 5;
    public int Height = 5;
    private GameObject[,] objectMatrix = new GameObject[5, 5];
    void Awake()
    {
        for (int i = 0; i < Width; i++)
        {
            for(int k = 0; k < Height; k++)
            {
                GameObject objectElement = (GameObject)Instantiate(Prefab);
                objectElement.transform.position = new Vector3(objectElement.transform.position.x + (i*XOffset), objectElement.transform.position.y + (k*XOffset), objectElement.transform.position.z);
                objectElement.transform.parent = this.transform;
                objectMatrix[i, k] = objectElement;
                
            }
        }
        this.transform.Rotate(0, 0, -90);
    }
    public void AppendLettersToObjectMatrix(char[,] array)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int k = 0; k < Height; k++)
            {
                objectMatrix[i, k].GetComponent<TextMesh>().text = array[i, k].ToString();
            }
        }
    }
    
}

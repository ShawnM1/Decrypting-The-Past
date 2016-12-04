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
    /// <summary>
    /// copies input array into scene array. Input array must be a 5x5 matrix
    /// </summary>
    /// <param name="array">2D array 5x5</param>
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
    void OnDrawGizmos()
    {

        Vector3 position = this.transform.position;
        Gizmos.color = Color.green;
        for (int i = 0; i < Width; i++)
        {
            for (int k = 0; k < Height; k++)
            {
                Gizmos.DrawWireCube(new Vector3(this.transform.position.x + (i * XOffset), this.transform.position.y + (k * XOffset), transform.position.z), new Vector3(5,5));
            }
        }
    }
}

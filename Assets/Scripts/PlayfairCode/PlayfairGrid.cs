using UnityEngine;
using System.Collections;

public class PlayfairGrid : MonoBehaviour {
    public char[,] ADFGX_array;
    public GameObject prefab;
    public int xOffset = 1;
    public int width = 5;
    public int height = 5;
    public bool showADFGX = false;
    private GameObject[,] objectMatrix = new GameObject[5, 5];
    private GameObject[,] ADFGXobjectMatrix = new GameObject[6, 6];
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DisableInput()
    {
        SetListenState(false);
    }
    public void EnableInput()
    {
        SetListenState(true);
    }
    void SetListenState(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<ADFGXButton>().SetListenState(state);
        }
    }
    void Awake()
    {
        for (int i = 0; i < width; i++)
        {
            for(int k = 0; k < height; k++)
            {
                /*GameObject objectElement = (GameObject)Instantiate(prefab);
                objectElement.transform.position = new Vector3(objectElement.transform.position.x + (i*xOffset), objectElement.transform.position.y + (k*xOffset), objectElement.transform.position.z);
                objectElement.transform.parent = this.transform;
                objectMatrix[i, k] = objectElement;
                */
            }
        }
        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 6; k++)
            {
                GameObject objectElement = (GameObject)Instantiate(prefab);
                objectElement.transform.position = new Vector3(objectElement.transform.position.x + (i * xOffset), objectElement.transform.position.y + (k * xOffset), objectElement.transform.position.z);
                objectElement.transform.parent = this.transform;
                ADFGXobjectMatrix[i, k] = objectElement;

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
    public void injectADFGX(char[,] array)
    {
        string ADFGX = " ADFGX";
        ADFGX_array = new char[6,6];
        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 6; k++)
            {
                if (i == 0 && k > 0)
                {
                    ADFGX_array[i, k] = ADFGX[k];
                }
                else if(k == 0 && i > 0)
                {
                    ADFGX_array[i, k] = ADFGX[i];
                }
                else
                {
                    if (i == 0 & k == 0)
                    {
                        ADFGX_array[i, k] = ' ';
                    }
                    if (i > 0 && k > 0)
                    {
                        ADFGX_array[i, k] = array[i - 1, k - 1];
                    }                    
                  
                }
            }
        }
        print(ADFGX_array);
        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 6; k++)
            {
                ADFGXobjectMatrix[i, k].GetComponent<TextMesh>().text = ADFGX_array[i, k].ToString();
            }
        }
    }
}

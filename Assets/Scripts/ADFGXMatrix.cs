using UnityEngine;
using System.Collections;

public class ADFGXMatrix : MonoBehaviour {
    public GameObject Prefab;
    public int XOffset = 1;
    public char[,] ADFGX_array;
    private GameObject[,] ADFGXobjectMatrix = new GameObject[6, 6];
    void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 6; k++)
            {
                GameObject objectElement = (GameObject)Instantiate(Prefab);
                objectElement.transform.position = new Vector3(transform.position.x + (i * XOffset), transform.position.y + (k * XOffset), transform.position.z);
                objectElement.transform.parent = this.transform;
                ADFGXobjectMatrix[i, k] = objectElement;

            }
        }
        this.transform.Rotate(0, 0, -90);
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
    public void injectADFGX(char[,] array)
    {
        string ADFGX = " ADFGX";
        ADFGX_array = new char[6, 6];
        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 6; k++)
            {
                if (i == 0 && k > 0)
                {
                    ADFGX_array[i, k] = ADFGX[k];
                }
                else if (k == 0 && i > 0)
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
    void OnDrawGizmos()
    {

        Vector3 position = this.transform.position;
        Gizmos.color = Color.green;
        for (int i = 0; i < 6; i++)
        {
            for (int k = 0; k < 6; k++)
            {
                Gizmos.DrawWireCube(new Vector3(this.transform.position.x + (i * XOffset), this.transform.position.y - (k * XOffset), transform.position.z), new Vector3(5, 5));
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransposeOrdering : MonoBehaviour
{

    // Use this for initialization
    ProblemHandler ProblemHandlerObj;

    void Start()
    {
        ProblemHandlerObj = GameObject.Find("ADFGX").GetComponent<ADFGX_Cipher>();
        print("KEY: " + ProblemHandlerObj.CurrentProblemData.key);
        
        UpdateUI();

    }

    public void UpdateUI()
    {
        for (int i = 0; i < 4; i++)
        {
            print(i + "keyindex" + "Assigned to: " + ProblemHandlerObj.CurrentProblemData.key[i].ToString());
            GameObject.Find(i + "keyindex").GetComponent<Text>().text = ProblemHandlerObj.CurrentProblemData.key[i].ToString();
        }
        ClearFields();
    }
    public void ClearFields()
    {
        for (int i = 0; i < 4; i++)
        {
            print(i + "keyindex" + "Assigned to: " + ProblemHandlerObj.CurrentProblemData.key[i].ToString());
            GameObject.Find("InputField" + (i + 1)).GetComponent<InputField>().text = "";
        }
    }
    void OnEnable()
    {
        UpdateUI();
        ClearFields();
    }
}

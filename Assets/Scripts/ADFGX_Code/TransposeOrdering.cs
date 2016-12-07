using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Orders the table rows by the key index. For example,
/// if the key is 3214, input field 1 is labeled "3"
/// </summary>
public class TransposeOrdering : MonoBehaviour
{
    // Use this for initialization
    ProblemHandler ProblemHandlerObj;
    void Start()
    {
        ProblemHandlerObj = GameObject.Find("ADFGX").GetComponent<ADFGX_Cipher>();
        UpdateUI();

    }

    public void UpdateUI()
    {
        for (int i = 0; i < 4; i++)
        {
           // print(i + "keyindex" + "Assigned to: " + ProblemHandlerObj.CurrentProblemData.key[i].ToString());
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

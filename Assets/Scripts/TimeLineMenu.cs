using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeLineMenu : MonoBehaviour {

    public Button BtnCaesarCipher;
    public Button BtnPlayFairCipher;
    public Button BtnADFGXCipher;

	void Start () {
       if (SaveContainer.Instance.SaveFile.CaesarCompleted)
        {
            BtnPlayFairCipher.interactable = true;
            if(SaveContainer.Instance.SaveFile.PlayfairCompleted)
            {
                BtnADFGXCipher.interactable = true;
            }
        }
	}
}

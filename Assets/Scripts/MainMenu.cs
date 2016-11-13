using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Button LoadButton;
	// Use this for initialization
	void Start () {
	    if(SaveContainer.DoSaveFilesExist())
        {
            LoadButton.interactable = true;
        }
	}
    public void NewGame()
    {
        //Prompt incase save file exists already
        SaveContainer.Instance.CreateNewSaveFile("test.dtp");
        StartCoroutine(GoToScene.GoToSceneEnumerator("CaesarCipher"));
    }

    public void ClickToLoadFile()
    {
        SaveContainer.Instance.LoadSaveFile("test.dtp");
    }

}

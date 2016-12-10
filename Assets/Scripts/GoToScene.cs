using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

	public void goToScene(string sceneName)
    {
        StartCoroutine(GoToSceneEnumerator(sceneName));
    }
    public static IEnumerator GoToSceneEnumerator(string sceneName)
    {
        if(sceneName == "MainMenuScene")
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Data");
            for(int i = 0; i < objs.Length; i++)
            {
                Destroy(objs[i]);
            }
        }
        Time.timeScale = 1;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        yield return async;
        Debug.Log("Loading complete");
    }
    public void ExitToOS()
    {
        Application.Quit();
    }
}

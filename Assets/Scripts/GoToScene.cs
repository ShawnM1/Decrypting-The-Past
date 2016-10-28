using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

	public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitToOS()
    {
        Application.Quit();
    }
}

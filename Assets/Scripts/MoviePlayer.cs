using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoviePlayer : MonoBehaviour {

    MovieTexture movie;
    GameObject GOM;
    RawImage rImage;
    bool firstStart = true;
	// Use this for initialization
	void Start () {
        //movie = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        GOM = GameObject.Find("GOM");
        rImage = GetComponent<RawImage>();
        movie = (MovieTexture)rImage.mainTexture;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    if(!movie.isPlaying && gameObject.activeSelf && !firstStart)
        {
            gameObject.SetActive(false);
            GOM.GetComponent<AudioSource>().UnPause();
            HUD.UnHideEverything();
            HUD.SetTutorialButtonState(true);
        }
        if (movie.isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            movie.Stop();
        }
	}
    public void playMovie()
    {
        // this line of code will make the Movie Texture begin playing
        //((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        firstStart = false;
        gameObject.SetActive(true);
        HideHUD();
        GOM.GetComponent<AudioSource>().Pause();
        movie.Stop();
        movie.Play();
        
        
        GetComponent<AudioSource>().Play();
  

    }
    void HideHUD()
    {
        if(gameObject.activeSelf && HUD.Hidden != true)
        {
            HUD.HideEverything();
            HUD.Hidden = true;
            HUD.SetTutorialButtonState(false);
        }
    }
}

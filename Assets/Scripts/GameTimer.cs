using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

    public static float Ticks
    {
        get
        {
            return timer;
        }
    }
    public static bool timerActive = true;
    static float timer = 0;
    string minutes;
    string seconds;
	
	// Update is called once per frame
	void Update () {
        if (timerActive)
        {
            timer += Time.deltaTime;
            minutes = Mathf.Floor(timer / 60).ToString("00");
            seconds = (timer % 60).ToString("00");
        }
    }
}

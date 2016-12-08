using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

    public static bool timerActive = true;
    static float timer = 0;
    static string minutes;
    static string seconds;
	
	// Update is called once per frame
	void Update () {
        if (timerActive)
        {
            timer += Time.deltaTime;
            minutes = Mathf.Floor(timer / 60).ToString("00");
            seconds = (timer % 60).ToString("00");
        }
    }
    public static float getTimeInSeconds()
    {
        return timer;
    }
    public static string GetTimeString()
    {
        return minutes + ":" + seconds;
    }
    public static void StopTimer()
    {
        timerActive = false;
    }
}

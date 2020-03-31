using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {
    
    public static bool checkTogBGMOn=true;
    public static int BGM=50;
    public static bool checkTogEffOn=true;
    public static int eff=50;
    public static bool inBossZone = false;

    public AudioSource audio;

    bool audioPlaying;
	// Use this for initialization
	void Start () {
        Time.timeScale=1;
        GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        audioPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(checkTogBGMOn)
            GetComponent<AudioSource>().volume = BGM * 0.01f;
        else
            GetComponent<AudioSource>().volume = 0;
        if (inBossZone == true && audioPlaying == false)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            audio.Play();
            audioPlaying = true;
        }
        else if (audioPlaying==true && inBossZone == false)
        {
            audio.Stop();
            gameObject.GetComponent<AudioSource>().Play();
            audioPlaying = false;
        }
    }
}

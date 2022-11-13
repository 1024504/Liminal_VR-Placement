using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundTimerTest : MonoBehaviour
{
    public float time = 7f; //seconds
    public AudioSource bubbleSound;


    private void Start()
    {
	    CallTimer();
	    /*GameManager gm = GameManager.Singleton;
	    gm.DoThing += CallTimer;*/
    }

    void CallTimer()
    {
	    StartCoroutine(Timer(time));
    }
    

    private IEnumerator Timer(float delay)
    {
	    yield return new WaitForSeconds(delay);
	    Debug.Log("Bloop!");
	    float pitch = Random.Range(0.55f, 1.1f);
	    this.bubbleSound.pitch = pitch;
	    this.bubbleSound.Play();
	    time = Random.Range(5f, 16f);
	    CallTimer();
    }
}

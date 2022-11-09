using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTimerTest : MonoBehaviour
{
    public float time = 7f; //seconds
    public AudioSource bubbleSound;

    public void Update()
    {
        float pitch;

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Bloop!");
            pitch = Random.Range(0.55f, 1.1f);
            this.bubbleSound.pitch = pitch;
            this.bubbleSound.Play();
            time = Random.Range(5f, 16f);      //reset timer
        }
    }

}

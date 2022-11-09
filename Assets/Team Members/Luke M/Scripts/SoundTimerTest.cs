using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTimerTest : MonoBehaviour
{
    public float time = 1f; //seconds
    public AudioSource bubbleSound;

    public void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Play Audio Here -- Timer Over!!");
            this.bubbleSound.Play();
            time = 1f;
        }
    }

}

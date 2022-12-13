using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandomiser : MonoBehaviour
{
    public float time = 7f; //seconds
    public AudioSource SFXSounds;


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
        float pitch = Random.Range(0.85f, 1.25f);
        this.SFXSounds.pitch = pitch;
        time = 7;
        CallTimer();
    }
}

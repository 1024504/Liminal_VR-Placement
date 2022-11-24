using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSongSelfDestruct : MonoBehaviour
{
	private AudioSource source;
	private float clipLength;
	
    void Awake()
    {
	    source = GetComponent<AudioSource>();
	    clipLength = source.clip.length;
	    Destroy(gameObject, clipLength);
    }
}

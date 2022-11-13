using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Singleton;

	public Action DoThing;
	
    // Start is called before the first frame update
    void Awake()
    {
	    Singleton = this;
    }
}

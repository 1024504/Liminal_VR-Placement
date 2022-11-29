using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForward : MonoBehaviour
{
	public float newTimescale = 2;

	public void ChangeTimescale()
	{
		Time.timeScale = newTimescale;
	}
	
	public void ResetTimescale()
	{
		Time.timeScale = 1;
	}
}

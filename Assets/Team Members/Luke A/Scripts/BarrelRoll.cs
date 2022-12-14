using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRoll : MonoBehaviour
{
	private Transform _transform;
	
	public Transform[] lWings;
	public Transform[] rWings;

	public float maxExtension;
	public float activationDelay;
	public float totalDuration;
	
	void Start()
	{
		_transform = transform;
		StartCoroutine(DelayActivation());
	}

	IEnumerator DelayActivation()
	{
		yield return new WaitForSeconds(activationDelay);
		foreach (var animator in GetComponentsInChildren<Animator>())
		{
			animator.enabled = false;
		}
		StartCoroutine(EnterBarrelRoll());
		StartCoroutine(PerformBarrelRoll());
	}

	IEnumerator EnterBarrelRoll()
	{
		float delay = totalDuration/8f;
		float progress = 0;
		while (progress < delay)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			for (int i = 0; i < lWings.Length; i++)
			{
				lWings[i].Rotate(new Vector3(0,0,maxExtension*Time.fixedDeltaTime/delay));
				rWings[i].Rotate(new Vector3(0,0,maxExtension*Time.fixedDeltaTime/delay));
			}
		}

		yield return new WaitForSeconds(totalDuration*3/4);
		StartCoroutine(ExitBarrelRoll());
	}
	
	IEnumerator PerformBarrelRoll()
	{
		float progress = 0;
		while (progress < totalDuration)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			_transform.Rotate(new Vector3(0,0,360*Time.fixedDeltaTime/totalDuration));
		}
	}
	
	IEnumerator ExitBarrelRoll()
	{
		float delay = totalDuration/8f;
		float progress = 0;
		while (progress/delay < 1f)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			for (int i = 0; i < lWings.Length; i++)
			{
				lWings[i].Rotate(new Vector3(0,0,-maxExtension*Time.fixedDeltaTime/delay));
				rWings[i].Rotate(new Vector3(0,0,-maxExtension*Time.fixedDeltaTime/delay));
			}
		}
	}
}

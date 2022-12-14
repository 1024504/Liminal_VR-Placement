using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishFollower : MonoBehaviour
{
	public Transform parentFish;
	private Transform _transform;

	public float interpolationStrength = 0.1f;
	public Vector3 offset;
	
	private void OnEnable()
	{
		_transform = transform;
		parentFish = _transform.parent;
		StartCoroutine(DelayAnimation());
	}

	private IEnumerator DelayAnimation()
	{
		yield return new WaitForSeconds(Random.Range(0f,0.5f));
		GetComponentInChildren<Animator>().enabled = true;
	}

	void Update()
	{
		_transform.position = Vector3.Lerp(_transform.position, parentFish.position+parentFish.TransformDirection(offset), interpolationStrength);
		_transform.rotation = Quaternion.Slerp(_transform.rotation, parentFish.rotation, interpolationStrength);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFollow : MonoBehaviour
{
	public Transform cameraTransform;
	public Vector3 offset;
	
	public float allowedIdleTime;
	public float lerpFactor;

	private float _idleTime = 0;

	private void Update()
	{
		_idleTime += Time.deltaTime;
		transform.rotation = Quaternion.LookRotation(cameraTransform.position-transform.position);
	}
}

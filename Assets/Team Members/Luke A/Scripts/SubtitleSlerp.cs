using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleSlerp : MonoBehaviour
{
	public Transform cameraTransform;
	private Transform _transform;
	private float _time;
	public float _delay = 3;
	public float lerpFactor = 0.01f;
	public float distance = 1.5f;
	private void Start()
	{
		_transform = transform;
	}

	void Update()
	{
		Vector3 camPosition = cameraTransform.position;
		Vector3 position = _transform.position;
		float angle = Vector3.Angle(cameraTransform.forward, position - camPosition);
		if (angle > 10)
		{
			_time += Time.deltaTime;
			
		}
		if(angle < 2)
		{
			_time = 0;
		}
		if (_time < _delay) return;
		_transform.position = Vector3.Slerp(position, camPosition+cameraTransform.forward*distance, lerpFactor);
		_transform.rotation = Quaternion.Slerp(_transform.rotation, cameraTransform.rotation, lerpFactor);
		}
}

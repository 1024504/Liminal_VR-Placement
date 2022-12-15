using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleSlerp : MonoBehaviour
{
	public Transform cameraTransform;
	private Transform _transform;

	private void Start()
	{
		_transform = transform;
	}

	void Update()
	{
		Vector3 camPosition = cameraTransform.position;
		Vector3 position = _transform.position;
		_transform.position = Vector3.Slerp(position, camPosition+cameraTransform.forward, 0.1f);
		// float angle = Mathf.Lerp(0,Vector3.SignedAngle(cameraTransform.forward, position - camPosition, Vector3.up),0.1f);
		// _transform.RotateAround(camPosition, Vector3.up, -angle);
		// angle = Mathf.Lerp(0,Vector3.SignedAngle(cameraTransform.forward, position - camPosition, Vector3.right),0.1f);
		// _transform.RotateAround(camPosition, Vector3.right, -angle);
		// Quaternion rotation = _transform.rotation;
		// _transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
	}
}

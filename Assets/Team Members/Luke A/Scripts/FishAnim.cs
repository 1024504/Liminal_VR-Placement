using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnim : MonoBehaviour
{
	private Transform _transform;
	public Transform[] ForwardTransforms;
	public Transform[] RearTransforms;

	public float maxWiggleAngle;
	public float wigglePeriod;

	private void Start()
	{
		_transform = transform;
	}

	void FixedUpdate()
    {
	    for (int i = 1; i < ForwardTransforms.Length; i++)
	    {
		    Vector3 oldRotation = ForwardTransforms[i-1].eulerAngles;
		    ForwardTransforms[i].eulerAngles = new Vector3(oldRotation.x, oldRotation.y+maxWiggleAngle*Mathf.Sin(Time.time/wigglePeriod), oldRotation.z);
	    }
	    for (int i = 1; i < RearTransforms.Length; i++)
	    {
		    Vector3 oldRotation = RearTransforms[i-1].eulerAngles;
		    RearTransforms[i].eulerAngles = new Vector3(oldRotation.x, oldRotation.y-maxWiggleAngle*Mathf.Sin(Time.time/wigglePeriod), oldRotation.z);
	    }
    }
}

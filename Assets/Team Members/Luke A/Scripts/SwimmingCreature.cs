using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingCreature : MonoBehaviour
{
	public float swimmingSpeed;
	public float turningSpeed;
	public List<Vector3> bezierPoints = new List<Vector3>();

	public bool handlesAlwaysOn; //dev
	
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    Vector3 BezierResult(float progress)
    {
	    return Vector3.zero;
    }
}

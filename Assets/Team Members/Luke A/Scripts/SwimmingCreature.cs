using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingCreature : MonoBehaviour
{
	public float swimmingSpeed;
	[Range(0,1)]
	public float turningStrength;
	public List<Vector3> bezierPoints = new List<Vector3>();

	public bool handlesAlwaysOn; //dev

	private Vector3[] _tempPoints1;
	private Vector3[] _tempPoints2;
	public Vector3[] bezierCurve;
	public int stepsPerPoint = 10;

	protected Transform _transform;
	private Rigidbody _rb;
	
	private int _nextPoint = 1;

	void Start()
	{
		_transform = transform;
		_rb = GetComponent<Rigidbody>();
        CalculateBezierCurve();
        _transform.position = bezierCurve[0];
        _transform.rotation = Quaternion.LookRotation(bezierCurve[1] - bezierCurve[0]);
	}

    void FixedUpdate()
    {
	    Vector3 position = _transform.position;
	    Vector3 nextPosition = _transform.TransformDirection(bezierCurve[_nextPoint]);
	    Vector3 heading = Vector3.Normalize(nextPosition - position));
	    float distanceToNextPoint = Vector3.Distance(position, nextPosition);
	    if (swimmingSpeed/60f < Vector3.Distance(position, nextPosition))
	    {
		    _rb.MovePosition(position + heading*(swimmingSpeed/60f));
	    }
	    else
	    {
		    _rb.MovePosition(position + heading*distanceToNextPoint);
		    if (++_nextPoint >= bezierCurve.Length)
		    {
			    _nextPoint = 1;
		    }
	    }
	    _rb.MoveRotation(Quaternion.Slerp(_transform.rotation,Quaternion.LookRotation(heading),turningStrength));
    }

    void CalculateBezierCurve()
    {
	    int count = bezierPoints.Count;
	    bezierCurve = new Vector3[count*stepsPerPoint];
	    for (int i = 0; i < bezierCurve.Length; i++)
	    {
		    _tempPoints1 = bezierPoints.ToArray();
		    for (int j = 1; j < count; j++)
		    {
			    _tempPoints2 = new Vector3[count - j];
			    for (int k = 0; k < _tempPoints2.Length; k++)
			    {
				    _tempPoints2[k] = Vector3.Lerp(_tempPoints1[k], _tempPoints1[k+1], i * 1f/(count*stepsPerPoint-1));
			    }
			    _tempPoints1 = _tempPoints2;
		    }
		    bezierCurve[i] = _tempPoints2[0];
	    }
    }
}

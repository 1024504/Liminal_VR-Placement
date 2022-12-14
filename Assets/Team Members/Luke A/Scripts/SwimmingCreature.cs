using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingCreature : MonoBehaviour
{
	public bool invisibleOnStart;
	
	public float swimmingSpeed;
	[Range(0,1)]
	public float turningStrength;
	public List<Vector3> bezierPoints = new List<Vector3>();

	public bool loopPath = true;

	private Vector3[] _tempPoints1;
	private Vector3[] _tempPoints2;
	public Vector3[] bezierCurve;
	public int stepsPerPoint = 10;

	protected Transform _transform;
	protected Rigidbody _rb;

	public float _framerate = 72f;
	
	protected int _nextPoint = 1;

	[Serializable]
	public struct SpeedTiming
	{
		public float delaySinceLastChange;
		public float newSwimmingSpeed;
	}

	public List<SpeedTiming> SpeedTimings;

	private SkinnedMeshRenderer[] _mesh;
	
	protected virtual void Start()
	{
		_mesh = GetComponentsInChildren<SkinnedMeshRenderer>();
		_transform = transform;
		_rb = GetComponent<Rigidbody>();
        CalculateBezierCurve();
        _transform.position = bezierCurve[0];
        _transform.rotation = Quaternion.LookRotation(bezierCurve[1] - bezierCurve[0]);
        if (SpeedTimings.Count > 0) StartCoroutine(ChangeSpeed());
	}

	private IEnumerator ChangeSpeed()
	{
		for (int i = 0; i < SpeedTimings.Count; i++)
		{
			yield return new WaitForSeconds(SpeedTimings[i].delaySinceLastChange);
			swimmingSpeed = SpeedTimings[i].newSwimmingSpeed;
			if (!invisibleOnStart) continue;
			if (i != 0) continue;
			foreach (SkinnedMeshRenderer mesh in _mesh)
			{
				mesh.enabled = true;
			}
		}
	}

    protected virtual void FixedUpdate()
    {
	    if(swimmingSpeed == 0) return;
	    if(_nextPoint < bezierCurve.Length) MoveAlongPath();
	    else if (loopPath)
	    {
		    _nextPoint = 0;
		    MoveAlongPath();
	    }
    }

    protected virtual void MoveAlongPath()
    {
	    Vector3 position = _transform.position;
	    Vector3 heading = Vector3.Normalize(bezierCurve[_nextPoint] - position);
	    float distanceToNextPoint = Vector3.Distance(position, bezierCurve[_nextPoint]);
	    float distanceToMove = swimmingSpeed * Time.fixedDeltaTime / _framerate;
	    if (distanceToMove < distanceToNextPoint)
	    {
		    _rb.MovePosition(position + heading*(distanceToMove));
	    }
	    else
	    {
		    _rb.MovePosition(position + heading*distanceToNextPoint);
		    _nextPoint++;
	    }

	    Quaternion newRotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(heading), turningStrength);
	    _rb.MoveRotation(newRotation);
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

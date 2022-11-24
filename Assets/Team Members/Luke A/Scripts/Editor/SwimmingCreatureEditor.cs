using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SwimmingCreature))]
public class SwimmingCreatureEditor : Editor //dev
{
	private Vector3[] _bezierPoints;
	private Vector3[] _tempPoints2;
	private Vector3[] _tempPoints1;

	private Vector3[] _bezierCurve;
	
	private void OnSceneGUI()
	{
		Handles.color = Color.magenta;
		SwimmingCreature obj = (SwimmingCreature) target;
		int count = obj.bezierPoints.Count;
		
		_bezierCurve = new Vector3[count*obj.stepsPerPoint];
		_bezierPoints = new Vector3[count];
		
		for (int i=0; i<count; i++)
		{
			obj.bezierPoints[i] = Handles.PositionHandle(obj.transform.TransformDirection(obj.bezierPoints[i]), Quaternion.identity);
			Handles.Label(obj.bezierPoints[i], i.ToString());
			_bezierPoints[i] = obj.bezierPoints[i];
		}

		for (int i = 0; i < _bezierCurve.Length; i++)
		{
			_tempPoints1 = _bezierPoints;
			for (int j = 1; j < count; j++)
			{
				_tempPoints2 = new Vector3[count - j];
				for (int k = 0; k < _tempPoints2.Length; k++)
				{
					_tempPoints2[k] = Vector3.Lerp(_tempPoints1[k], _tempPoints1[k+1], i * 1f/(count*obj.stepsPerPoint-1));
				}
				_tempPoints1 = _tempPoints2;
			}
			_bezierCurve[i] = _tempPoints2[0];
		}

		obj.bezierCurve = _bezierCurve;
		Handles.DrawPolyLine(_bezierCurve);
	}
}

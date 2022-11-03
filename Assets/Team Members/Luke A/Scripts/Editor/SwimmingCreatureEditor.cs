using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SwimmingCreature))]
public class SwimmingCreatureEditor : Editor //dev
{
	private Vector3[] bezierPoints;
	private Vector3[] tempPoints2;
	private Vector3[] tempPoints1;

	private Vector3[] bezierCurve;
	
	private void OnSceneGUI()
	{
		Handles.color = Color.magenta;
		SwimmingCreature obj = (SwimmingCreature) target;
		int count = obj.bezierPoints.Count;
		
		bezierCurve = new Vector3[count*11];
		bezierPoints = new Vector3[count];
		
		for (int i=0; i<count; i++)
		{
			obj.bezierPoints[i] = Handles.PositionHandle(obj.bezierPoints[i], Quaternion.identity);
			Handles.Label(obj.bezierPoints[i], i.ToString());
			bezierPoints[i] = obj.bezierPoints[i];
		}

		for (int i = 0; i < bezierCurve.Length; i++)
		{
			tempPoints1 = bezierPoints;
			for (int j = 1; j < count; j++)
			{
				tempPoints2 = new Vector3[count - j];
				for (int k = 0; k < tempPoints2.Length; k++)
				{
					tempPoints2[k] = Vector3.Lerp(tempPoints1[k], tempPoints1[k+1], i * 1f/(count*11-1));
				}
				tempPoints1 = tempPoints2;
			}
			bezierCurve[i] = tempPoints2[0];
		}
		Handles.DrawPolyLine(bezierCurve);
	}
}

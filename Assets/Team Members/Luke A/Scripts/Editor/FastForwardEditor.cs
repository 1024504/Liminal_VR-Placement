using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[CustomEditor(typeof(FastForward))]
public class FastForwardEditor : Editor //dev
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Change Timescale")) ((FastForward)target).ChangeTimescale();
		if (GUILayout.Button("Reset Timescale")) ((FastForward)target).ResetTimescale();
	}
}

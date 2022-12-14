using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishFollowerManager : MonoBehaviour
{
	public Vector3 edge;
	public Vector3 spread;
	private FishFollower[] _fish;

	private void OnEnable()
	{
		_fish = GetComponentsInChildren<FishFollower>();
		for (int i=0; i < _fish.Length; i++)
		{
			_fish[i].interpolationStrength = Random.Range(0.005f, 0.1f);
			_fish[i].offset = new Vector3(edge.x+(i*spread.x+spread.x*Random.Range(0f,1f))/_fish.Length,Random.Range(edge.y, edge.y+spread.y),Random.Range(edge.z, edge.z+spread.z));
		}
	}

	void Update()
	{
		
	}
}

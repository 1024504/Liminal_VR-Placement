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
		_fish = FindObjectsOfType<FishFollower>();
		for (int i=0; i < _fish.Length; i++)
		{
			_fish[i].interpolationStrength = Random.Range(0.005f, 0.1f);
			_fish[i].offset = new Vector3(edge.x+spread.x*(i+i%3)/_fish.Length,edge.y+spread.y*(Mathf.Pow(i,2)-i)/(Mathf.Pow(_fish.Length,2)-i),edge.z+spread.z*Mathf.Pow(i,2)/Mathf.Pow(_fish.Length,2));
		}
	}

	void Update()
	{
		
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantaRayGlow : MonoBehaviour
{
	public float lengthOfGlow;
	
	private Renderer _renderer;
	
	private readonly Color _noGlowColour = Color.black;
	private readonly Color _fullGlowColour = new Color(0.9f, 0.9f, 0.9f, 1f);
	
	private void OnEnable()
	{
		_renderer = GetComponent<Renderer>();
		StartCoroutine(ChangeColour(lengthOfGlow));
	}

	IEnumerator ChangeColour(float delay)
	{
		float progress = 0;
		while (progress/delay < 0.5f)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			_renderer.material.SetColor("_EmissionColor", Color.Lerp(_noGlowColour, _fullGlowColour, progress*2/delay));
		}
		progress = 0;
		while (progress/delay < 0.5f)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			_renderer.material.SetColor("_EmissionColor", Color.Lerp(_fullGlowColour, _noGlowColour, progress*2/delay));
		}

		gameObject.SetActive(false);
	}
}

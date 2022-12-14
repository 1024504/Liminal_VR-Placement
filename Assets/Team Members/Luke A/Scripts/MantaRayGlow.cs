using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantaRayGlow : MonoBehaviour
{
	public float activationDelay;
	public float glowDuration;
	public float dimDuration;
	public float finalScale;
	
	private Renderer _renderer;
	private Transform _transform;
	
	private readonly Color _noGlowColour = Color.black;
	private readonly Color _fullGlowColour = new Color(0f, 0.3725f, 0.3867f, 1f);
	
	private void OnEnable()
	{
		_renderer = GetComponentInChildren<Renderer>();
		_renderer.enabled = false;
		_transform = transform;
		StartCoroutine(DelayActivation(activationDelay));
	}

	IEnumerator DelayActivation(float delay)
	{
		yield return new WaitForSeconds(delay);
		_renderer.enabled = true;
		StartCoroutine(ChangeColour(glowDuration, _noGlowColour, _fullGlowColour, true));
		StartCoroutine(ChangeScale());
	}

	IEnumerator ChangeColour(float delay, Color oldColor, Color newColor, bool reverse)
	{
		float progress = 0;
		while (progress/delay < 1f)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			_renderer.material.SetColor("_EmissionColor", Color.Lerp(oldColor, newColor, progress/delay));
		}

		if (reverse) StartCoroutine(ChangeColour(dimDuration, newColor, oldColor, false));
		else gameObject.SetActive(false);
	}

	IEnumerator ChangeScale()
	{
		float totalDuration = glowDuration + dimDuration;
		float progress = 0;
		while (progress/totalDuration < 1f)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			_transform.localScale = Vector3.Lerp(Vector3.one*0.9f, Vector3.one*finalScale, progress/totalDuration);
		}
	}
}

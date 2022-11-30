using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBreathing : MonoBehaviour
{
	public Transform cameraTransform;

	private Renderer _renderer;

	private readonly Color _breatheColour = new Color(0f, 0.8f, 1f, 1f);
	private readonly Color _holdColour = new Color(0.8f, 0f, 1f, 1f);

	public List<SizeChanging> sizeChanges;

	[Serializable]
	public struct SizeChanging
	{
		public float holdDelay;
		public float changeDelay;
		public float newScale;
	}
	
	void OnEnable()
	{
		_renderer = GetComponentInChildren<Renderer>();
		if (sizeChanges.Count > 0) StartCoroutine(ChangeSizes());
	}

	private void Update()
	{
		transform.rotation = Quaternion.LookRotation(cameraTransform.position-transform.position);
	}

	IEnumerator ChangeSizes()
	{
		for (int i = 0; i < sizeChanges.Count; i++)
		{
			StartCoroutine(ChangeColour(sizeChanges[i].holdDelay));
			yield return new WaitForSeconds(sizeChanges[i].holdDelay);
			float oldScale = transform.localScale.x;
			float newScale = sizeChanges[i].newScale;
			float progress = 0;
			float totalDelay = sizeChanges[i].changeDelay;
			while (progress/totalDelay < 1)
			{
				yield return new WaitForFixedUpdate();
				progress += Time.fixedDeltaTime;
				transform.localScale = Mathf.Lerp(oldScale, newScale, progress/totalDelay) * Vector3.one;
			}
		}
	}

	IEnumerator ChangeColour(float delay)
	{
		float progress = 0;
		while (progress/0.5f < 1)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			_renderer.material.SetColor("_EmissionColor", Color.Lerp(_breatheColour, _holdColour, progress/0.5f));
		}
		yield return new WaitForSeconds(delay-1f);
		progress = 0;
		while (progress/0.5f < 1)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			_renderer.material.SetColor("_EmissionColor", Color.Lerp(_holdColour, _breatheColour, progress/0.5f));
		}
	}
}

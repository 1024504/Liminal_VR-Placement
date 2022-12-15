using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBreathing : MonoBehaviour
{
	public Transform cameraTransform;

	public float scalingPower = 1;
	
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
			// StartCoroutine(ChangeColour(sizeChanges[i].holdDelay));
			yield return new WaitForSeconds(sizeChanges[i].holdDelay);
			float oldScale = transform.localScale.x;
			float newScale = sizeChanges[i].newScale;
			float progress = 0;
			float totalDelay = sizeChanges[i].changeDelay;
			while (progress/totalDelay < 1)
			{
				yield return new WaitForFixedUpdate();
				transform.localScale = Mathf.Lerp(oldScale, newScale, 1-Mathf.Pow(1-progress/totalDelay,scalingPower)) * Vector3.one;
				progress += Time.fixedDeltaTime;
			}
		}

		StartCoroutine(FadeAway());
	}

	IEnumerator FadeAway()
	{
		float progress = 0;
		float duration = 4f;
		Color fade = new Color(0.7294f, 0.9921f, 1f, 0f);
		while (progress/duration < 1)
		{
			yield return new WaitForFixedUpdate();
			progress += Time.fixedDeltaTime;
			_renderer.material.color = Color.Lerp(_renderer.material.color, fade, Mathf.Pow(progress/duration, 2));
		}
	}
}

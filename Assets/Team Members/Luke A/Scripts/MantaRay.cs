using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantaRay : SwimmingCreature
{
	private Animator[] animators;

	protected override void Start()
	{
		animators = GetComponentsInChildren<Animator>();
		base.Start();
	}

	protected override void MoveAlongPath()
	{
		Vector3 position = _transform.position;
		Vector3 heading = Vector3.Normalize(bezierCurve[_nextPoint] - position);
		float distanceToNextPoint = Vector3.Distance(position, bezierCurve[_nextPoint]);
		if (swimmingSpeed/_framerate < Vector3.Distance(position, bezierCurve[_nextPoint]))
		{
			_rb.MovePosition(position + heading*(swimmingSpeed/_framerate));
		}
		else
		{
			_rb.MovePosition(position + heading*distanceToNextPoint);
			_nextPoint++;
		}

		Quaternion oldRotation = _transform.rotation;
		Quaternion newRotation = Quaternion.Slerp(oldRotation, Quaternion.LookRotation(heading), turningStrength);
		float horizontalAngle = (newRotation.eulerAngles.y - oldRotation.eulerAngles.y + 180) % 360 - 180;
		foreach (Animator animator in animators)
		{
			animator.SetFloat("Horizontal Angle", horizontalAngle);
		}
		_rb.MoveRotation(newRotation);
	}
}

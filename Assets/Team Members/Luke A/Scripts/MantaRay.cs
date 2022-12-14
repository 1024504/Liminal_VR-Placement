using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MantaRay : SwimmingCreature
{
	private Animator animator;

	protected override void Start()
	{
		animator = GetComponent<Animator>();
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
		
		
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
		Quaternion newRotation = Quaternion.LookRotation(heading);
		TurningAngle = Quaternion.RotateTowards(newRotation,oldRotation,360).eulerAngles;
		TurningAngle.x = (TurningAngle.x + 180) % 360 - 180;
		TurningAngle.y = (TurningAngle.y + 180) % 360 - 180;
		TurningAngle.z = (TurningAngle.z + 180) % 360 - 180;
		animator.SetFloat("Vertical Angle",TurningAngle.x);
		animator.SetFloat("Horizontal Angle",TurningAngle.y);
		_rb.AddTorque(new Vector3(0,TurningAngle.y*turningStrength-_rb.angularVelocity.y*0.95f,0));
	}
}

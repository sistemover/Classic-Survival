using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomocionMotor : MonoBehaviour 
{
	public GameObject cam;
	
	[Header("Inputs")]
	public float vertical;
	public float horizontal;
	public float moveAmount;
	public Vector3 moveDir;

	[Header("Stats")]
	public float moveSpeed = 0.7f;
	public float runSpeed = 3.5f;
	public float rotateSpeed = 50f;
	public float toGround = 0.5f;

	[Header ("Estados")]
	public bool onGround;
	public bool run;
	public bool lockOn;

	//VARIABLES PROPIAS
	Animator anim;
	Rigidbody rigid;

	float delta;
	float antMoveSpeed;

	GameObject CamA;
	GameObject CamB;

	Vector3 GroundNormal;
	LayerMask ignoreLayers;


	//**************************************************

	public void Init(Rigidbody r, Animator a)
	{
		antMoveSpeed = moveSpeed;
		SetRigidBody (r);
		SetAnimator (a);
		SetLayer ();
	}

	void SetAnimator(Animator a)
	{
		anim = a;
		anim.applyRootMotion = false;
	}

	void SetRigidBody(Rigidbody r)
	{
		rigid = r;
		rigid.angularDrag=999;
		rigid.drag=4;
		rigid.constraints=RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

	void SetLayer()
	{
		gameObject.layer = 8;
		ignoreLayers = ~(1 << 12);
		anim.SetBool ("onGround", true);
	}

	public void FixedTick(float d, Vector2 AxisL)
	{
		delta = d;
		horizontal = AxisL.x;
		vertical = AxisL.y;

		UpdateOrientation ();
		UpdateMoveDirection ();
		UpdateFalling ();
		UpdateSpeed ();
		UpdateRotation ();
		UpdateAnimation ();
	}

	public void Tick(float d)
	{
		delta = d;
		UpdateOnGround ();
	}

	void UpdateOrientation()
	{
		GameObject g = new GameObject ();
		Transform t_g = g.transform;
		Vector3 v_cam = new Vector3 (0, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
		t_g.eulerAngles = v_cam;

		Vector3 v = vertical * t_g.forward;
		Vector3 h = horizontal * t_g.right;

		Destroy (g);

		moveDir = v + h;
		moveDir.Normalize ();
	}

	void UpdateMoveDirection()
	{
		float m = Mathf.Abs (horizontal) + Mathf.Abs (vertical);
		moveAmount = Mathf.Clamp01(m);
		UpdateMoveSpeed ();
	}

	void UpdateMoveSpeed()
	{
		if (moveAmount > 0f) 
		{
			moveAmount = 1f;
			moveSpeed = 1.5f;
		}
		/*
		if (moveAmount >= 0.8f)
			moveSpeed = 1.5f;
		else if (moveAmount < 0.8f)
			moveSpeed = antMoveSpeed;*/
	}

	void UpdateFalling()
	{
		rigid.drag = (moveAmount > 0 || onGround == false) ? 0 : 4;
	}

	void UpdateSpeed()
	{
		float targetSpeed = moveSpeed;
		if (onGround)
			rigid.velocity = moveDir * (targetSpeed * moveAmount);
	}

	void UpdateRotation()
	{
		Vector3 targetDir = moveDir;
		targetDir.y = 0;
		if (targetDir == Vector3.zero)
			targetDir = transform.forward;
		Quaternion tr = Quaternion.LookRotation (targetDir);
		Quaternion targetRotation = Quaternion.Slerp (transform.rotation, tr, delta * moveAmount * rotateSpeed);
		transform.rotation = targetRotation;
	}

	void UpdateAnimation()
	{
		anim.SetFloat ("Vertical", moveAmount, 0.4f, delta);
	}

	void UpdateOnGround()
	{
		onGround = CheckOnGround ();
		anim.SetBool ("onGround", onGround);
	}

	bool CheckOnGround()
	{
		bool r = false;
		Vector3 origin = transform.position + (Vector3.up * toGround);
		Vector3 dir = -Vector3.up;
		float dis = toGround + 0.3f;

		RaycastHit hit;
		if (Physics.Raycast(origin, dir, out hit, dis, ignoreLayers)) 
		{
			r = true;
			Vector3 targetPosition = hit.point;
			transform.position = targetPosition;
		}
		return r;
	}
}

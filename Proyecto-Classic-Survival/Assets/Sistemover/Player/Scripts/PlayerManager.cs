using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	//VARIABLES PÚBLICAS
	public GameObject ActiveModel;
	[HideInInspector] public Animator m_Animator;
	[HideInInspector] public Rigidbody m_RigidBody;
	[HideInInspector] public CapsuleCollider m_CapsuleCollider;

	//INSTANCIADORES
	private LocomocionMotor m_LocomocionMotor;
	public LocomocionMotor LocomocionMotor
	{
		get
		{
			if (m_LocomocionMotor == null)
				m_LocomocionMotor = gameObject.GetComponent<LocomocionMotor> ();
			return m_LocomocionMotor;
		}
	}



	//**************************************************

	void Awake()
	{
		Debug.Log ("Awake PlayerManager");
		JoinPlayer ();
	}

	public void Init () 
	{	
		Debug.Log ("Start PlayerManager");
		Initi ();
		LocomocionMotor.Init (m_RigidBody, m_Animator);
	}

	void Initi()
	{
		m_Animator = ActiveModel.GetComponent<Animator> ();
		m_CapsuleCollider = GetComponent<CapsuleCollider> ();
		m_RigidBody = GetComponent<Rigidbody> ();
	}

	public void Tick (float d, bool d_a, bool d_b, bool d_x, bool d_y, bool u_a, bool u_b, bool u_x, bool u_y) 
	{
		LocomocionMotor.Tick (d);
	}

	public void FixedTick(float d, Vector2 AxisL, Vector2 AxisR, GameObject c)
	{
		LocomocionMotor.FixedTick (d, AxisL, c);
	}

	void JoinPlayer()
	{
		GameManager.instance.LocalPlayer = this;
	}
}

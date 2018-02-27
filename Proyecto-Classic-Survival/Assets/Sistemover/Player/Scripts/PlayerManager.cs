using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	//EVENTOS
	public event System.Action<LocomocionMotor> OnLocomocionMotorJoined;

	//INSTANCIADORES
	private LocomocionMotor m_LocalLocomocionMotor;
	public LocomocionMotor LocalLocomocionMotor
	{
		get{ return m_LocalLocomocionMotor; }
		set
		{
			m_LocalLocomocionMotor = value;
			if (OnLocomocionMotorJoined != null)
				OnLocomocionMotorJoined (LocalLocomocionMotor);
		}
	}



	//**************************************************

	void Awake()
	{
		JoinPlayer ();
	}

	void Start () 
	{		
	}

	public void FixedTick()
	{

	}

	public void Tick () 
	{
		if (LocalLocomocionMotor != null)
			LocalLocomocionMotor.Tick ();
	}

	void JoinPlayer()
	{
		SimpleGameManager.instance.LocalPlayer = this;
		Debug.Log ("Uniendo Player");
	}
}

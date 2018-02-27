using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	#region Singleton
	public static GameManager instance;
	void Awake ()
	{
		if (instance != null)
			return;
		instance = this;
	}
	#endregion

	//EVENTOS
	public event System.Action<PlayerManager> OnLocalPlayerJoined;

	//VARIABLES PÚBLICAS
	[HideInInspector]public InputManager InputManager;

	//INSTANCIADORES
	private  PlayerManager m_Player;
	public PlayerManager LocalPlayer
	{
		get
		{
			return m_Player;
		}
		set
		{
			m_Player = value;
			if (OnLocalPlayerJoined != null)
				OnLocalPlayerJoined (m_Player);
		}
	}

	//**************************************************

	void Start () 
	{
		Init ();
	}

	void Init()
	{
		InputManager = gameObject.GetComponent<InputManager> ();
	}

	void Update () 
	{
		float delta = Time.deltaTime;
		InputManager.Tick ();

		if (LocalPlayer != null)
			LocalPlayer.Tick (delta, 
				InputManager.d_a, InputManager.d_b, InputManager.d_x, InputManager.d_y, 
				InputManager.u_a, InputManager.u_b, InputManager.u_x, InputManager.u_y);
	}

	void FixedUpdate()
	{
		float fixedDelta = Time.fixedDeltaTime;
		InputManager.FixedTick ();

		if (LocalPlayer != null)
			LocalPlayer.FixedTick (fixedDelta, InputManager.AxisL, InputManager.AxisR);
	}
		
}

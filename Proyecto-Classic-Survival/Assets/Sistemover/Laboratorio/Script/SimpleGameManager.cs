using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGameManager : MonoBehaviour 
{
	#region Singleton
	public static SimpleGameManager instance;
	void Awake ()
	{
		if (instance != null) 
		{
			Debug.Log ("Hay'más de un GameManager instanciado!!");
			return;
		}
		instance = this;
	}
	#endregion

	//EVENTOS
	public event System.Action<PlayerManager> OnLocalPlayerJoined;


	//INSTANCIADORES
	private PlayerManager m_LocalPlayer;
	public PlayerManager LocalPlayer
	{
		get{return m_LocalPlayer; }
		set
		{
			m_LocalPlayer = value;
			if (OnLocalPlayerJoined != null)
				OnLocalPlayerJoined (m_LocalPlayer);
		}
	}

	//**************************************************

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if (LocalPlayer != null)
			LocalPlayer.Tick ();
	}
}

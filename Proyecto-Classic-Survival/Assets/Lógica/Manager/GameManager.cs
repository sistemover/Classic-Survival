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

	//VARIABLES PÚBLICAS
	[HideInInspector]public InputManager InputManager;

	//INSTANCIADORES


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
		InputManager.Tick ();
	}

	void FixedUpdate()
	{
		InputManager.FixedTick ();
	}
		
}

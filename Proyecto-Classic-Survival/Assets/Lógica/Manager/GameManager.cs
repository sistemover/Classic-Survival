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

	//**************************************************

	void Start () 
	{
		
	}
	
	void FixedUpdate()
	{
		
	}
	void Update () 
	{
		
	}
}

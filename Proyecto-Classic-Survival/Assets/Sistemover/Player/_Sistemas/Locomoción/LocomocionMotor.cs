using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomocionMotor : MonoBehaviour 
{
	void Awake ()
	{
		SimpleGameManager.instance.LocalPlayer.LocalLocomocionMotor = this;
	}

	public void FixedTick()
	{
		
	}

	public void Tick()
	{
		
	}
}

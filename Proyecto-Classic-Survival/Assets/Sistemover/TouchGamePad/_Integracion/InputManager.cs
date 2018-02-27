using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sistemover.TouchGamePad;

public class InputManager : MonoBehaviour 
{
	public bool d_x,d_y,d_a,d_b,u_x,u_y,u_a,u_b;
	public Vector2 AxisL, AxisR;

	//**************************************************

	public void FixedTick()
	{
		FixedGetInput();

		/* Debug: inputs tipo float.
		Debug.Log ("HR: " + AxisR.x + " | " + "VR: " + AxisR.y);
		/**/
	}

	public void Tick()
	{
		GetInput ();

		/* Debug: inputs tipo bool.
		Debug.Log ("HR: " + AxisR.x + " | " + "VR: " + AxisR.y);
		/**/
	}
		
	//floats
	void FixedGetInput()
	{
		AxisL = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal L"), CrossPlatformInputManager.GetAxis ("Vertical L"));
		AxisR = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal R"), CrossPlatformInputManager.GetAxis ("Vertical R"));
	}

	//bools
	void GetInput()
	{
		d_x = CrossPlatformInputManager.GetButtonDown ("X");
		u_x = CrossPlatformInputManager.GetButtonUp ("X");

		d_y = CrossPlatformInputManager.GetButtonDown ("Y");
		u_y = CrossPlatformInputManager.GetButtonUp ("Y");

		d_a = CrossPlatformInputManager.GetButtonDown ("A");
		u_a = CrossPlatformInputManager.GetButtonUp ("A");

		d_b = CrossPlatformInputManager.GetButtonDown ("B");
		u_b = CrossPlatformInputManager.GetButtonUp ("B");
	}
}

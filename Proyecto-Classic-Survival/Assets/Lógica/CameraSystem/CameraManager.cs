using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour 
{
	public GameObject[] Cameras;
	public int ActiveCamera;

	//**************************************************

	void Awake ()
	{
		JoinCamera ();
	}

	void JoinCamera()
	{
		GameManager.instance.ActualCameraManager = this;
	}
}

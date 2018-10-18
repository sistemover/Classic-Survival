using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour 
{
	//VARIABLES PÚBLICAS
	public GameObject[] Cameras;
	public int ActiveCamera;

	//**************************************************

	void Awake ()
	{
		JoinCamera ();
	}

	void Start()
	{
		DesactivarCamaras (ActiveCamera);
	}

	public void DesactivarCamaras(int a)
	{
		ActiveCamera = a;
		for (int i = 0; i < Cameras.Length; i++) {
			Cameras [i].SetActive (false);
		}
		Cameras [ActiveCamera].SetActive (true);
	}

	void JoinCamera()
	{
		GameManager.instance.ActualCameraManager = this;
	}
}

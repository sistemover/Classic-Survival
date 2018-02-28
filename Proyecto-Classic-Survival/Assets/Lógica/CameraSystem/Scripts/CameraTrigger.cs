using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour 
{
	//VARIABLES PÚBLICAS
	public int activeCamera;

	//VARIABLES PROPIAS
	CameraManager cameraManager;

	
	//**************************************************

	void Start () 
	{
		cameraManager = GameObject.Find ("CameraManager").GetComponent<CameraManager> ();
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Player"))
			SwitchCameras ();
	}

	void SwitchCameras()
	{
		cameraManager.DesactivarCamaras (activeCamera);
	}
}

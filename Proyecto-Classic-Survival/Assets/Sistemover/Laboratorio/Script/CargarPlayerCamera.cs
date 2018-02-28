using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargarPlayerCamera : MonoBehaviour 
{
	public GameObject player;
	public GameObject cameraManager;

	public void tick(bool B)
	{
		if(B)
		{
			player.SetActive(!player.activeSelf);
			cameraManager.SetActive(!cameraManager.activeSelf);
		}
	}
}

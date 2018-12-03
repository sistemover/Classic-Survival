using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteraction : MonoBehaviour 
{
	public string key;
	public List<Dialogos> dialogos;

	void Awake()
	{
		Debug.Log ("DIALOGADOR EN AWAKE");
	}
	void Start()
	{
		Debug.Log ("DIALOGADOR EN START");
	}
}

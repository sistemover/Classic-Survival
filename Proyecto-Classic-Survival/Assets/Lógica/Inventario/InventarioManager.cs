using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour 
{
	//Variables Públicas
	public List<Pocket> PocketContainer = new List<Pocket> ();

	//Delegates
	public delegate void OnItemChanged();
	public OnItemChanged onItemSelectedCallback;

	public void ActualizarInventario()
	{
		if (onItemSelectedCallback != null)
			onItemSelectedCallback.Invoke ();
	}
}

[System.Serializable]
public class Pocket
{
	public string ItemPath = "Items/";
	public int Amount;
}
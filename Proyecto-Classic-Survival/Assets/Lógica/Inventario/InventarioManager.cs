using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour 
{
	//Variables Públicas
	public List<PocketItem> PocketContainer = new List<PocketItem> ();

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
public class PocketItem
{
	public string ItemPath = "Items/";
	public int Amount;
}
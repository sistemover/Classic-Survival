using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	public string name_key;
	public int MaxAmount;
	public string IconoPequeño = "Sprites/Simples/";
	public string IconoGrande = "Sprites/Amplios/";
	public string Modelo = "Primitivas/";


	public virtual void Use()
	{
		Debug.Log ("Usado el Item: " + name_key);
	}
}

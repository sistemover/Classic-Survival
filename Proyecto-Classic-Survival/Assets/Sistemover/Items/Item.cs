using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	public string name_key;
	public string IconoPequeño = "Sprites/Simples/";
	public string IconoGrande = "Sprites/Amplios/";
	public string Modelo = "Primitivas/";

	public int MaxAmount;

	public bool isStackable = false;
	public bool isUsable = false;
	public bool isEquipment = false;
	public bool isCombinable = false;

	public string[] CombinableWith;

	public virtual void Use()
	{
		Debug.Log ("Usado el Item: " + name_key);
	}
	public virtual Equipment GetEquip()
	{
		return null;
	}
}

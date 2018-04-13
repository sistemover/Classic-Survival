using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour 
{
	#region Singleton
	public static EquipmentManager Singleton;
	void Awake()
	{
		if (Singleton != null)
			return;
		Singleton = this;
	}
	#endregion

	public Equipment[] currentEquipment;

	public void Init()
	{
		int numSlots = System.Enum.GetNames (typeof(EquipType)).Length;
		currentEquipment=new Equipment[numSlots];
	}
	public void Equip(Equipment newItem)
	{
		int slotIndex = (int)newItem.equipType;
		currentEquipment [slotIndex] = newItem;
	}
	public void RemoveEquip(Equipment newItem)
	{
		if (newItem == null)
			return;
		int slotIndex = (int)newItem.equipType;
		currentEquipment [slotIndex] = null;
	}
	public bool CheckIsEquip(Equipment e)
	{
		bool resultado = false;
		int slotIndex = (int)e.equipType;
		if (currentEquipment [slotIndex] == e)
			resultado = true;
		return resultado;
	}
	public void QuitarExtraEquip(List <PocketItem> container)
	{
		for (int i = 0; i < currentEquipment.Length; i++) 
		{
			bool estado = false;
			for (int e = 0; e < container.Count; e++) 
			{
				Equipment equip = LoaderManager.singleton.CargarItem(container[e].ItemPath).GetEquip();
				if (currentEquipment [i] == equip) 
				{
					estado = true;
					break;
				}
			}
			if (estado == false)
				RemoveEquip (currentEquipment [i]);
		}
	}
}

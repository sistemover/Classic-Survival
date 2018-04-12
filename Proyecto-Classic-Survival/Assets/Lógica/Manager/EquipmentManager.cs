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

	Equipment[] currentEquipment;

	public void init()
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
		int slotIndex = (int)newItem.equipType;
		currentEquipment [slotIndex] = null;
	}
}

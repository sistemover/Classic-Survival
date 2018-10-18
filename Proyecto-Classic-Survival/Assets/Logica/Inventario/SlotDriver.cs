using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotDriver : MonoBehaviour, IDropHandler
{
	public SlotType slotType;
	public ItemDriver itemDriver;

	ItemDriver dropDriver;
	ItemDriver hostDriver;
	InventarioManager inventarioManager;

	void Awake()
	{
		itemDriver.mySlotType = slotType;
	}
	public void OnDrop(PointerEventData eventData)
	{
		Init (eventData);
		inventarioManager.DropItemControl (dropDriver, hostDriver);
		/*
		if (!slotType.Equals (SlotType.Equip)) 
		{
			inventarioManager.DropItemControl (dropDriver, hostDriver);
			Debug.Log ("No soy EquipSlot");
		} 
		else 
		{
			Debug.Log ("Si soy EquipSlot");
			if (dropDriver.myItem.isEquipment) 				
				inventarioManager.DropEquipControl (dropDriver, hostDriver);
		}*/
	}
	void Init(PointerEventData eventData)
	{
		dropDriver = eventData.pointerDrag.GetComponent<ItemDriver> ();
		hostDriver = GetComponentInChildren<ItemDriver> ();
		inventarioManager = GameManager.instance.inventarioManager;
	}
}

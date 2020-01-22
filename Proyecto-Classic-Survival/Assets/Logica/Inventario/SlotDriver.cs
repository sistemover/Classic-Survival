using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotDriver : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public SlotType slotType;
	public ItemDriver itemDriver;

	ItemDriver dropDriver;
	ItemDriver hostDriver;
	InventarioManager inventarioManager;
	Item dropItem;

	GameManager gameManager
	{
		get
		{
			return GameManager.instance;
		}
	}
	ColorManager colorManager
	{
		get
		{
			return ColorManager.Singleton;
		}
	}
	private Image slotImage
	{
		get
		{
			return gameObject.GetComponent<Image>();
		}
	}

	void Awake()
	{
		itemDriver.mySlotType = slotType;
	}
	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("*** Inicia OnDrop!!");
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
		gameManager.inventarioManager.GuardarInventario();
		Debug.Log("*** Completa OnDrop!!");
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.pointerDrag == null)
			return;
		Init(eventData);
		if (dropItem==null)
			return;
		if (slotType.Equals(SlotType.Equip))
		{
			if (!dropItem.isEquipment)
				slotImage.color = colorManager.Denied;
			else
				slotImage.color = colorManager.Approved;
		}
		else if (slotType.Equals(SlotType.Pocket))
			slotImage.color = colorManager.Pocket;
		else if (slotType.Equals(SlotType.Pickup))
			slotImage.color = colorManager.Pickup;
		else if (slotType.Equals(SlotType.Storage))
			slotImage.color = colorManager.Storage;

	}
	public void OnPointerExit(PointerEventData eventData)
	{
		slotImage.color = ColorManager.Singleton.Normal;
	}
	void Init(PointerEventData eventData)
	{
		dropDriver = eventData.pointerDrag.GetComponent<ItemDriver> ();
		hostDriver = GetComponentInChildren<ItemDriver> ();
		inventarioManager = gameManager.inventarioManager;
		if (dropDriver.myItem != null)
			dropItem = dropDriver.myItem;
	}
}

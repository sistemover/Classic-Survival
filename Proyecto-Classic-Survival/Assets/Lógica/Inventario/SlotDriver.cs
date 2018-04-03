using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotDriver : MonoBehaviour, IDropHandler
{
	public SlotType slotType;
	public ItemDriver itemDriver;
	public Image highlight;

	ItemDriver droppedItemDriver;
	PocketItem droppedPocketItem;
	ItemDriver hostItemDriver;
	PocketItem hostPocketItem;

	InventarioManager inventarioManager;
	InventarioCanvasManager inventarioCanvasManager;

	void Awake()
	{
		itemDriver.mySlotType = slotType;
	}
	public void OnDrop(PointerEventData eventData)
	{
		Init (eventData);
		if (hostPocketItem == null) 
		{
			inventarioManager.PocketContainer.Remove (droppedPocketItem);
			inventarioManager.PocketContainer.Add (droppedPocketItem);
			inventarioManager.ActualizarInventario ();
			inventarioCanvasManager.SeleccionarSlot (inventarioManager.PocketContainer.Count-1);
		}
	}
	void Init(PointerEventData eventData)
	{
		droppedItemDriver = eventData.pointerDrag.GetComponent<ItemDriver> ();
		droppedPocketItem = droppedItemDriver.myPocketItem;
		hostItemDriver = GetComponentInChildren<ItemDriver> ();
		hostPocketItem = hostItemDriver.myPocketItem;

		inventarioManager = GameManager.instance.inventarioManager;
		inventarioCanvasManager = GameManager.instance.canvasManager.inventarioCanvasManager;
	}
}

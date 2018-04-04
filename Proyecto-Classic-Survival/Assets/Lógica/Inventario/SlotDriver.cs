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

	List<PocketItem> pocketContainer;

	void Awake()
	{
		itemDriver.mySlotType = slotType;
	}
	public void OnDrop(PointerEventData eventData)
	{
		Init (eventData);
		DropToVoid ();
	}
	void Init(PointerEventData eventData)
	{
		droppedItemDriver = eventData.pointerDrag.GetComponent<ItemDriver> ();
		droppedPocketItem = droppedItemDriver.myPocketItem;
		hostItemDriver = GetComponentInChildren<ItemDriver> ();
		hostPocketItem = hostItemDriver.myPocketItem;

		inventarioManager = GameManager.instance.inventarioManager;
		inventarioCanvasManager = GameManager.instance.canvasManager.inventarioCanvasManager;

		pocketContainer = inventarioManager.PocketContainer;
	}
	void DropToVoid()
	{
		if (hostPocketItem == null) 
		{
			pocketContainer.Remove (droppedPocketItem);

			Item LastItem = GetItem (pocketContainer.Count - 1);
			if (CheckStackable (droppedItemDriver.myItem, LastItem))
				SumarAmount (LastItem.MaxAmount, droppedPocketItem, pocketContainer[pocketContainer.Count-1]);

			if(droppedPocketItem.Amount != 0)
				pocketContainer.Add (droppedPocketItem);

			inventarioManager.ActualizarInventario ();
			inventarioCanvasManager.SeleccionarSlot (pocketContainer.Count-1);
		}
	}
	bool CheckStackable(Item a, Item b)
	{
		bool result = false;
		if (!a.isStackable && !b.isStackable)
			return result;
		if (!(a.name_key.Equals (b.name_key)))
			return result;
		return true;
	}
	Item GetItem(int index)
	{
		string path = pocketContainer [index].ItemPath;
		return Resources.Load(path) as Item;
	}
	void SumarAmount(int MaxAmount, PocketItem drop, PocketItem host)
	{
		int suma = host.Amount + drop.Amount;
		int dif = suma - MaxAmount;
		if (dif <= 0) 
		{
			host.Amount = suma;
			drop.Amount = 0;
		}
		else 
		{
			host.Amount = MaxAmount;
			drop.Amount = dif;
		}
	}
}

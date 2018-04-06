using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour 
{
	//Variables Públicas
	public List<PocketItem> PocketContainer = new List<PocketItem> ();
	public List<PocketItem> PickupContainer = new List<PocketItem> ();
	public List<PocketItem> EquipContainer = new List<PocketItem> ();
	public List<PocketItem> StorageContainer = new List<PocketItem> ();

	//DROP MANAGER
	ItemDriver dropDriver;
	PocketItem dropPocket;
	ItemDriver hostDriver;
	PocketItem hostPocket;

	List<PocketItem> dropContainer;
	List<PocketItem> hostContainer;

	ItemDriver[] dropItemsDriver;
	ItemDriver[] hostItemsDriver;

	InventarioCanvasManager inventarioCanvasManager;
	CombinationManager combinationManager;

	public void Init()
	{
		inventarioCanvasManager = GameManager.instance.canvasManager.inventarioCanvasManager;
		combinationManager = GetComponent<CombinationManager> ();
		//combinationManager.OpenDictionary ();
	}
	public void ActualizarInventario()
	{
		inventarioCanvasManager.CargarPocketsContainers ();
	}

	public void DropManager(ItemDriver dropDriver, ItemDriver hostDriver)
	{
		Init (dropDriver, hostDriver);
		DropToVoid ();
		DropToHost ();
	}

	void Init(ItemDriver drop, ItemDriver host)
	{
		dropDriver = drop;
		dropPocket = drop.myPocketItem;
		hostDriver = host;
		hostPocket = host.myPocketItem;

		SeteandoContainer ();
	}
	void SeteandoContainer()
	{
		switch (dropDriver.mySlotType) 
		{
			case SlotType.Pocket:
				dropContainer = PocketContainer;
				dropItemsDriver = inventarioCanvasManager.PocketItemsDriver;
				break;
			case SlotType.Pickup:
				dropContainer = PickupContainer;
				dropItemsDriver = inventarioCanvasManager.PickupItemsDriver;
				break;
			case SlotType.Equip:
				dropContainer = EquipContainer;
				dropItemsDriver = inventarioCanvasManager.EquipItemsDriver;
				break;
			case SlotType.Storage:
				dropContainer = StorageContainer;
				dropItemsDriver = inventarioCanvasManager.StorageItemsDriver;
				break;
			default:
				Debug.Log ("No hay contenedor");
				break;
		}
		switch (hostDriver.mySlotType) 
		{
			case SlotType.Pocket:
				hostContainer = PocketContainer;
				hostItemsDriver = inventarioCanvasManager.PocketItemsDriver;
				break;
			case SlotType.Pickup:
				hostContainer = PickupContainer;
				hostItemsDriver = inventarioCanvasManager.PickupItemsDriver;
				break;
			case SlotType.Equip:
				hostContainer = EquipContainer;
				hostItemsDriver = inventarioCanvasManager.EquipItemsDriver;
				break;
			case SlotType.Storage:
				hostContainer = StorageContainer;
				hostItemsDriver = inventarioCanvasManager.StorageItemsDriver;
				break;
			default:
				Debug.Log ("No hay contenedor");
				break;
		}
	}
	void DropToVoid()
	{
		if (hostPocket == null) 
		{
			dropContainer.Remove (dropPocket);
			hostContainer.Add (dropPocket);

			ActualizarInventario ();
			inventarioCanvasManager.SeleccionarSlot (hostContainer.Count-1, hostItemsDriver);
		}
	}
	void DropToHost()
	{
		if (hostPocket != null) 
		{
			if (hostDriver.myItem == null)
				return;
			if (CheckStackable (hostDriver.myItem, dropDriver.myItem))
			{
				Acumular ();
				return;
			}
			else if(CheckCombinable())
			{
				Debug.Log ("Son Combinables");
				dropContainer.Remove (dropPocket);
				hostPocket.ItemPath = combinationManager.ActualCombinationItem.itemPath;
				hostPocket.Amount = combinationManager.ActualCombinationItem.amount;
				combinationManager.CloseDictionary ();
				ActualizarInventario ();
				hostItemsDriver [GetContainerIndex (hostPocket, hostItemsDriver)].TapSeleccionarItem ();
			}
			else
			{
				Cambiar ();
			}
		}
	}
	void Acumular()
	{
		SumarAmount (hostDriver.myItem.MaxAmount, dropPocket, hostPocket);
		if (dropPocket.Amount <= 0) 
		{
			dropContainer.Remove (dropPocket);
			ActualizarInventario ();
			hostItemsDriver [GetContainerIndex (hostPocket, hostItemsDriver)].TapSeleccionarItem ();
			return;
		}
		ActualizarInventario ();
		hostDriver.TapSeleccionarItem ();
		return;
	}
	void Cambiar()
	{
		string dropPath = dropPocket.ItemPath;
		string hostPath = hostPocket.ItemPath;
		int dropAmount = dropPocket.Amount;
		int hostAmount = hostPocket.Amount;

		dropPocket.ItemPath = hostPath;
		hostPocket.ItemPath = dropPath;
		dropPocket.Amount = hostAmount;
		hostPocket.Amount = dropAmount;

		ActualizarInventario ();
		hostDriver.TapSeleccionarItem ();
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
	bool CheckCombinable()
	{
		bool result = true;
		combinationManager.OpenDictionary ();
		if (!ExistCombination (dropDriver.myItem, hostDriver.myItem)) 
		{
			if(!ExistCombination (hostDriver.myItem, dropDriver.myItem))
			{
				combinationManager.CloseDictionary ();
				result = false;
			}
		}
		return result;
	}
	int GetContainerIndex(PocketItem pocket, ItemDriver[] itemsDrivers)
	{		
		for (int i = 0; i < itemsDrivers.Length; i++) 
		{
			if (itemsDrivers [i].myPocketItem == pocket) 
				return i;
		}
		return 0;
	}
	bool ExistCombination(Item Base, Item Reactivo)
	{
		bool result = false;
		if (!Base.isCombinable)
			return result;
		for (int i = 0; i < Base.CombinableWith.Length; i++) 
		{		
			string base_key = Base.CombinableWith [i];
			string need_key = combinationManager.GetCombinationItem (base_key);
			if (need_key == Reactivo.name_key) 
			{
				result = true;
				break;
			}
		}
		return result;
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
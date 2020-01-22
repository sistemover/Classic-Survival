using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioCanvasManager : MonoBehaviour 
{
	public DescriptionManager[] descriptionManager;
	public Transform PocketItemDriverParent;
	public Transform PickupItemDriverParent;
	public Transform EquipItemDriverParent;
	public Transform StorageItemDriverParent;

	[HideInInspector] public ItemDriver[] PocketItemsDriver;
	[HideInInspector] public ItemDriver[] PickupItemsDriver;
	[HideInInspector] public ItemDriver[] EquipItemsDriver;
	[HideInInspector] public ItemDriver[] StorageItemsDriver;

	GameManager gameManager;
	InventarioManager inventarioManager;
	public void Init()
	{
		Debug.Log ("Start InventarioCanvasManager");
		gameManager = GameManager.instance;
		inventarioManager = gameManager.inventarioManager;
		PocketItemsDriver = PocketItemDriverParent.GetComponentsInChildren<ItemDriver> ();
		PickupItemsDriver = PickupItemDriverParent.GetComponentsInChildren<ItemDriver> ();
		EquipItemsDriver = EquipItemDriverParent.GetComponentsInChildren<ItemDriver> ();
	}
	public void CargarPocketsContainers()
	{
		CargandoPocketContainer ();
		CargandoPickupContainer ();
		CargandoEquipContainer ();
	}
	void CargandoPocketContainer()
	{
		List<PocketItem> Container = inventarioManager.PocketContainer;

		CheckAmount (Container);

		for (int i = 0; i < Container.Count; i++)
			PocketItemsDriver [i].AgregarItem (Container [i]);

		for (int i = Container.Count; i < PocketItemsDriver.Length; i++)
			PocketItemsDriver [i].LimpiarSlot ();
	}
	void CargandoPickupContainer()
	{
		List<PocketItem> Container = inventarioManager.PickupContainer;

		if (Container.Count == 0)
			gameManager.canvasManager.TapPickup ();

		CheckAmount (Container);

		for (int i = 0; i < Container.Count; i++) {
			PickupItemsDriver[i].ActivarDesactivarSlot (true);
			PickupItemsDriver [i].AgregarItem (Container [i]);
		}
		for (int i = Container.Count; i < PickupItemsDriver.Length; i++)
			PickupItemsDriver[i].ActivarDesactivarSlot (false);
	}
	void CargandoEquipContainer()
	{
		List<PocketItem> Container = inventarioManager.EquipContainer;

		for (int i = 0; i < Container.Count; i++) 
		{
			Equipment equip = LoaderManager.Singleton.CargarItem (Container [i].ItemPath).GetEquip ();
			if (!EquipmentManager.Singleton.CheckIsEquip (equip)) 
				equip.Use ();
		}
		EquipmentManager.Singleton.QuitarExtraEquip (Container);

		for (int i = 0; i < Container.Count; i++)
			EquipItemsDriver [i].AgregarItem (Container [i]);

		for (int i = Container.Count; i < EquipItemsDriver.Length; i++)
			EquipItemsDriver [i].LimpiarSlot ();
	}
	void CheckAmount(List<PocketItem> Container)
	{
		Debug.Log("Inicia CheckAmount");
		for (int i = 0; i < Container.Count; i++) 
		{
			int amount = Container[i].Amount;
			bool isStackable = LoaderManager.Singleton.CargarItem (Container [i].ItemPath).isStackable;
			if (amount == 0 && isStackable)
				Container.Remove (Container [i]);
		}
		Debug.Log("Completa CheckAmount");
	}
	public void SeleccionarSlot(int index, ItemDriver[] itemDrivers)
	{
		itemDrivers [index].TapSeleccionarItem ();
	}
	public int GetEquipDriverID(string name)
	{
		int id = 0;
		for (int i = 0; i <EquipItemsDriver.Length; i++) 
		{ 
			string driverName = EquipItemsDriver[i].GetComponentInParent<SlotDriver> ().name;
			if (driverName.Equals (name))
				id = i;
		}
		return id;
	}
}

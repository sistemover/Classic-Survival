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

	InventarioManager inventarioManager
	{
		get
		{
			return GameManager.instance.inventarioManager;
		}
	}
	public void Init()
	{
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

		for (int i = 0; i < Container.Count; i++) 
			if (Container [i].Amount == 0)
				Container.Remove (Container [i]);

		for (int i = 0; i < Container.Count; i++)
			PocketItemsDriver [i].AgregarItem (Container [i]);

		for (int i = Container.Count; i < PocketItemsDriver.Length; i++)
			PocketItemsDriver [i].LimpiarSlot ();
	}
	void CargandoPickupContainer()
	{
		List<PocketItem> Container = inventarioManager.PickupContainer;

		if (Container.Count == 0)
			GameManager.instance.canvasManager.TapPickup ();

		for (int i = 0; i < Container.Count; i++) 
			if (Container [i].Amount == 0)
				Container.Remove (Container [i]);

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
			if (Container [i].Amount == 0)
				Container.Remove (Container [i]);

		for (int i = 0; i < Container.Count; i++)
			EquipItemsDriver [i].AgregarItem (Container [i]);

		for (int i = Container.Count; i < EquipItemsDriver.Length; i++)
			EquipItemsDriver[i].LimpiarSlot ();
	}
	public void SeleccionarSlot(int index, ItemDriver[] itemDrivers)
	{
		itemDrivers [index].TapSeleccionarItem ();
	}
}

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

		inventarioManager.onItemSelectedCallback += Tick;
	}
	public void Tick()
	{
		CargandoPocketContainer ();
		CargandoPickupContainer ();
	}
	void CargandoPocketContainer()
	{
		List<PocketItem> Container = inventarioManager.PocketContainer;

		for (int i = 0; i < Container.Count; i++) 
			PocketItemsDriver [i].AgregarItem (Container [i]);

		for (int i = Container.Count; i < PocketItemsDriver.Length; i++)
			PocketItemsDriver [i].LimpiarSlot ();
	}
	void CargandoPickupContainer()
	{
		List<PocketItem> Container = inventarioManager.PickupContainer;

		for (int i = 0; i < Container.Count; i++) 
			PickupItemsDriver[i].AgregarItem (Container [i]);

		for (int i = Container.Count; i < PickupItemsDriver.Length; i++)
			PickupItemsDriver[i].LimpiarSlot ();
	}
	public void SeleccionarSlot(int index, ItemDriver[] itemDrivers)
	{
		itemDrivers [index].TapSeleccionarItem ();
	}
}

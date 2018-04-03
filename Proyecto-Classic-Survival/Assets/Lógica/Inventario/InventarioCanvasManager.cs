using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioCanvasManager : MonoBehaviour 
{
	public DescriptionManager[] descriptionManager;
	public Transform PocketItemDriverParent;

	[HideInInspector] public ItemDriver[] PocketItemsDriver;

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
		inventarioManager.onItemSelectedCallback += Tick;
	}

	public void Tick()
	{
		CargarItems ();
	}

	void CargarItems()
	{
		List<PocketItem> pocketContainer = inventarioManager.PocketContainer;

		for (int i = 0; i < pocketContainer.Count; i++) 
			PocketItemsDriver [i].AgregarItem (pocketContainer [i]);
		
		for (int i = pocketContainer.Count; i < PocketItemsDriver.Length; i++)
			PocketItemsDriver [i].LimpiarSlot ();
	}

	public void SeleccionarSlot(int index)
	{
		PocketItemsDriver [index].TapSeleccionarItem ();
	}
}

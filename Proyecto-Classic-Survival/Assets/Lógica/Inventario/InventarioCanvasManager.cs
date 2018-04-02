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
		SeleccionarPrimerSlot ();
	}

	void CargarItems()
	{
		List<Pocket> pocketContainer = inventarioManager.PocketContainer;
		for (int i = 0; i < pocketContainer.Count; i++) 
			PocketItemsDriver [i].AgregarItem (pocketContainer [i].ItemPath, pocketContainer [i].Amount);
		for (int i = 0; i < PocketItemsDriver.Length; i++)
		{
			if (PocketItemsDriver [i].myItem == null)
				PocketItemsDriver [i].LimpiarSlot ();
		}
	}

	void SeleccionarPrimerSlot()
	{
		PocketItemsDriver [0].TapSeleccionarItem ();
	}
}

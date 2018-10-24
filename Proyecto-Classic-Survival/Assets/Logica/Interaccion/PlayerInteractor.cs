using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour 
{
	GameManager gameManager;
	CanvasManager canvasManager;
	LevelManager actualLevelManager;
	InventarioManager inventarioManager;
	InventarioCanvasManager inventarioCanvasManager;

	//Interacciones
	bool isPicking;
	bool isDoor;

	List <int> Candidatos;
	Transform Interaction;

	public void Init () 
	{
		gameManager = GameManager.instance;
		canvasManager = gameManager.canvasManager;
		actualLevelManager = gameManager.ActualLevelManager;
		inventarioManager = gameManager.inventarioManager;
		inventarioCanvasManager = canvasManager.inventarioCanvasManager;
	}
	public void SetLevelInteraction(Transform interaction, string key)
	{
		GameObject a = gameManager.touchGamePadManager.A;
		Text t = a.GetComponentInChildren<Text> ();
		if (interaction == null || key == null) 
			return;
		if (!a.activeInHierarchy)
			a.SetActive (true);
		switch (key) 
		{
		case "Door":
			t.text = "D";
			Interaction = interaction;
			isDoor = true;
			break;
		default:
			t.text = "A";
			break;
		}
	}
	public void SetObjectInteraction(List <int> id, string key)
	{
		Candidatos = new List<int> ();
		GameObject a = gameManager.touchGamePadManager.A;
		Text t = a.GetComponentInChildren<Text> ();
		if (id == null || key == null)
			return;
		if (!a.activeInHierarchy)
			a.SetActive (true);		
		switch (key) 
		{
			case "Pickable":
				t.text = "P";
				Candidatos = id;
				isPicking = true;
				break;
			default:
				t.text = "A";
				break;
		}
	}

	public void Tick (bool d_a, bool u_a) 
	{
		if (u_a) 
		{
			if (isPicking)
				IPicking ();
			if (isDoor)
				IDoor ();
		}
	}
	void IPicking()
	{
		List <PocketItem> pickupContainer = inventarioManager.PickupContainer;
		pickupContainer.Clear ();
		int Round = actualLevelManager.Round;
		ObjectsData[] levelObjects = actualLevelManager.GamePlus [Round].LevelObjects;

		for (int i = 0; i < Candidatos.Count; i++) 
		{
			PocketItem localCandidato = new PocketItem ();
			for (int e = 0; e < levelObjects.Length; e++) 
			{
				if (Candidatos[i] == levelObjects[e].ID) 
				{
					localCandidato.id = levelObjects [e].ID;
					localCandidato.ItemPath = levelObjects [e].path;
					localCandidato.Amount = levelObjects [e].amount;
					break;
				}
			}
			pickupContainer.Add (localCandidato);
		}
		canvasManager.TapInventario ();
		canvasManager.TapPickup ();
		if (canvasManager.MenuInventario.activeInHierarchy) 
		{
			GameManager.instance.inventarioManager.ActualizarInventario ();
			canvasManager.inventarioCanvasManager.SeleccionarSlot (0, inventarioCanvasManager.PickupItemsDriver);
		}
		isPicking = false;
	}
	void IDoor()
	{
		
		Debug.Log ("Interacción de Door: " + Interaction.name);
		isDoor = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour 
{
	GameManager gameManager
	{
		get
		{
			return GameManager.instance;
		}
	}
	void OnMouseDown()
	{
		CanvasManager canvasManager = gameManager.canvasManager;
		GameObject menuInventario = canvasManager.MenuInventario;
		canvasManager.TapInventario ();
		if (menuInventario.activeInHierarchy) 
		{
			gameManager.inventarioManager.ActualizarInventario ();
			canvasManager.inventarioCanvasManager.SeleccionarSlot (0, canvasManager.inventarioCanvasManager.PocketItemsDriver);
		}
	}
}

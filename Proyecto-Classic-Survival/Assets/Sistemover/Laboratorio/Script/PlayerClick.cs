using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour 
{
	void OnMouseDown()
	{
		CanvasManager canvasManager = GameManager.instance.canvasManager;
		GameObject menuInventario = canvasManager.MenuInventario;
		canvasManager.TapInventario ();
		if (menuInventario.activeInHierarchy) 
		{
			GameManager.instance.inventarioManager.ActualizarInventario ();
			canvasManager.inventarioCanvasManager.SeleccionarSlot (0, canvasManager.inventarioCanvasManager.PocketItemsDriver);
		}
	}
}

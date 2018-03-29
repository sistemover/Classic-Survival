using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour 
{
	void OnMouseDown()
	{
		GameManager.instance.canvasManager.TapInventario ();
	}
}

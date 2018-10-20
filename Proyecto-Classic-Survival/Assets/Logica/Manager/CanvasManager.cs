using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour 
{
	//Variables públicas
	public GameObject Interfaz;
	public GameObject MenuInicio;
	public GameObject MenuOpciones;
	public GameObject MenuIdioma;
	public GameObject MenuInventario;
	public GameObject MenuExaminar;
	public GameObject MenuPausa;
	public GameObject MenuPickup;

	//Variables Privadas
	private GameManager gameManager;

	//Instanciaciones
	private TouchGamePadManager m_touchGamePadManager;
	public TouchGamePadManager touchGamePadManager
	{
		get
		{
			if (m_touchGamePadManager == null)
				m_touchGamePadManager = gameObject.GetComponent<TouchGamePadManager> ();
			return m_touchGamePadManager;
		}
	}
	private InventarioCanvasManager m_inventarioCanvasManager;
	public InventarioCanvasManager inventarioCanvasManager
	{
		get
		{
			if (m_inventarioCanvasManager == null)
				m_inventarioCanvasManager = gameObject.GetComponent<InventarioCanvasManager> ();
			return m_inventarioCanvasManager;
		}
	}


	public void Init()
	{
		Debug.Log ("Start CanvasManager");
		gameManager = GameManager.instance;
		InstanciarMenus ();
		touchGamePadManager.ActivarDesactivarGamePad (false);//Apaga el GamePad
		inventarioCanvasManager.Init();
	}

	void InstanciarMenus()//Setea los Menús, para poder luego cargar los idiomas.
	{
		GameObject[] menus = { MenuInicio, MenuOpciones, MenuIdioma, MenuInventario, MenuExaminar, Interfaz, MenuPausa,MenuPickup};
		for (int i = 0; i < menus.Length; i++) 
		{
			menus [i].SetActive (true);
			if (i > 0)//Se preocupa de Dejar activado solamente el menú de inicio.
				menus [i].SetActive (false);
		}
	}
	public void TapIniciar()
	{
		gameManager.inventarioManager.CargarInventario ();
		gameManager.touchGamePadManager.ActivarDesactivarGamePad (MenuInicio.activeInHierarchy);
		Interfaz.SetActive (MenuInicio.activeInHierarchy);
		MenuInicio.SetActive (!MenuInicio.activeInHierarchy);
		Time.timeScale = 1f;

		gameManager.sceneController.InitiateScene ();
	}
	public void TapOpciones()
	{
		MenuOpciones.SetActive (!MenuOpciones.activeInHierarchy);
	}
	public void TapIdioma()
	{
		MenuIdioma.SetActive (!MenuIdioma.activeInHierarchy);
	}
	public void TapInventario()
	{
		gameManager.touchGamePadManager.ActivarDesactivarRightGamePad (MenuInventario.activeInHierarchy);
		Interfaz.SetActive (!Interfaz.activeInHierarchy);
		MenuInventario.SetActive (!MenuInventario.activeInHierarchy);
		if (!MenuInventario.activeInHierarchy) 
		{
			gameManager.inventarioManager.GuardarInventario ();
			Debug.Log ("Cerrando Inventario");
		}
		//Abriendo Pickup
		//TapPickup();
	}
	public void TapPickup()
	{
		if (gameManager.inventarioManager.PickupContainer.Count == 0) 
		{
			gameManager.touchGamePadManager.ActivarDesactivarLeftGamePad (true);
			MenuPickup.SetActive (false);
		} 
		else 
		{
			gameManager.touchGamePadManager.ActivarDesactivarLeftGamePad (MenuPickup.activeInHierarchy);
			MenuPickup.SetActive (!MenuPickup.activeInHierarchy);
		}
	}
	public void TapExaminar()
	{
		TapInventario ();

		gameManager.touchGamePadManager.ActivarDesactivarGamePad (false);
		
		if(MenuInventario.activeInHierarchy)
			gameManager.touchGamePadManager.ActivarDesactivarLeftGamePad (true);

		Interfaz.SetActive (false);

		MenuExaminar.SetActive (!MenuExaminar.activeInHierarchy);
	}
	public void TapPausa()
	{
		if (MenuPausa.activeInHierarchy)
			Time.timeScale = 1f;
		else
			Time.timeScale = 0f;
		gameManager.touchGamePadManager.ActivarDesactivarGamePad (MenuPausa.activeInHierarchy);
		gameManager.LocalPlayer.GetComponent<PlayerClick> ().enabled = MenuPausa.activeInHierarchy;
		Interfaz.SetActive (!Interfaz.activeInHierarchy);
		MenuPausa.SetActive (!MenuPausa.activeInHierarchy);
	}
	public void TapMenuPrincipal()
	{
		gameManager.inventarioManager.CargarInventario ();
		gameManager.touchGamePadManager.ActivarDesactivarGamePad (MenuInicio.activeInHierarchy);
		Interfaz.SetActive (MenuInicio.activeInHierarchy);
		MenuInicio.SetActive (!MenuInicio.activeInHierarchy);
		Time.timeScale = 1f;

		gameManager.sceneController.QuitScene ();

		MenuPausa.SetActive (!MenuPausa.activeInHierarchy);
	}
}

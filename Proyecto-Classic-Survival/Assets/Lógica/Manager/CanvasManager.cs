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
		gameManager = GameManager.instance;
		InstanciarMenus ();
		touchGamePadManager.ActivarDesactivarGamePad (false);//Apaga el GamePad
		inventarioCanvasManager.Init();
	}

	void InstanciarMenus()
	{
		GameObject[] menus = { MenuInicio, MenuOpciones, MenuIdioma, MenuInventario, MenuExaminar, Interfaz, MenuPausa };
		for (int i = 0; i < menus.Length; i++) 
		{
			menus [i].SetActive (true);
			if (i > 0)
				menus [i].SetActive (false);
		}
	}
	public void TapIniciar()
	{
		CargarPlayerCamera cargar = gameManager.cargar;
		GameObject player = cargar.player;
		GameObject cameraManager = cargar.cameraManager;

		gameManager.touchGamePadManager.ActivarDesactivarGamePad (MenuInicio.activeInHierarchy);
		cameraManager.SetActive(!cameraManager.activeSelf);
		player.SetActive(!player.activeSelf);
		Interfaz.SetActive (MenuInicio.activeInHierarchy);
		MenuInicio.SetActive (!MenuInicio.activeInHierarchy);
		Time.timeScale = 1f;
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
	}
	public void TapExaminar()
	{		
		gameManager.touchGamePadManager.ActivarDesactivarLeftGamePad (MenuExaminar.activeInHierarchy);
		MenuInventario.SetActive (!MenuInventario.activeInHierarchy);
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
		TapIniciar ();
		MenuPausa.SetActive (!MenuPausa.activeInHierarchy);
	}
}

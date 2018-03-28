using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour 
{
	public GameObject MenuInicio;
	public GameObject MenuOpciones;
	public GameObject MenuIdioma;

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

	public void Init()
	{
		InstanciarMenus ();

		touchGamePadManager.ActivarDesactivarGamePad (false);//Apaga el GamePad
	}

	void InstanciarMenus()
	{
		GameObject[] menus = { MenuInicio, MenuOpciones, MenuIdioma };
		for (int i = 0; i < menus.Length; i++) 
		{
			menus [i].SetActive (true);
			if (i > 0)
				menus [i].SetActive (false);
		}
	}
	public void TapIniciar()
	{
		GameManager gameManager = GameManager.instance;
		CargarPlayerCamera cargar = gameManager.cargar;
		GameObject player = cargar.player;
		GameObject cameraManager = cargar.cameraManager;

		gameManager.touchGamePadManager.ActivarDesactivarGamePad (true);
		cameraManager.SetActive(!cameraManager.activeSelf);
		player.SetActive(!player.activeSelf);
		MenuInicio.SetActive (false);
	}
	public void TapOpciones()
	{
		MenuOpciones.SetActive (!MenuOpciones.activeInHierarchy);
	}
	public void TapIdioma()
	{
		MenuIdioma.SetActive (!MenuIdioma.activeInHierarchy);
	}
}

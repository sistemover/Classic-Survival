using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour 
{
	//Variables públicas
	public GameObject Subtitulos;
	public GameObject Interfaz;
	public GameObject MenuInicio;
	public GameObject MenuOpciones;
	public GameObject MenuIdioma;
	public GameObject MenuInventario;
	public GameObject MenuExaminar;
	public GameObject MenuPausa;
	public GameObject MenuPickup;
	public GameObject MenuResolucion;

	//Variables Privadas
	private GameManager gameManager;
	private LoaderManager loaderManager;
	private List<GameObject> allMenus;
	private List<GameObject> openMenus;

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
		loaderManager = LoaderManager.Singleton;
		InstanciarMenus ();
		touchGamePadManager.ActivarDesactivarGamePad (false);//Apaga el GamePad
		inventarioCanvasManager.Init();
	}

	void InstanciarMenus()//Setea los Menús, para poder luego cargar los idiomas.
	{
		allMenus = new List<GameObject>
		{ 
			MenuInicio, 
			MenuOpciones, 
			MenuIdioma,
			MenuInventario, 
			MenuExaminar, 
			Interfaz, 
			MenuPausa,
			MenuPickup,
			Subtitulos,
			MenuResolucion,
		};
		for (int i = 0; i < allMenus.Count; i++) 
		{
			allMenus [i].SetActive (true);
			if (i > 0)//Se preocupa de Dejar activado solamente el menú de inicio.
				allMenus [i].SetActive (false);
		}
	}
	void AbrirCerrarInterfaz(bool b)
	{
		if (openMenus == null)
			return;
		for (int i = 0; i < openMenus.Count; i++)
			openMenus [i].SetActive (b);
	}
	void GuardarTodaInterfazAbierta()
	{
		openMenus = new List<GameObject> ();
		for (int i = 0; i < allMenus.Count; i++) 
			if (allMenus[i].activeInHierarchy) 
				openMenus.Add (allMenus [i]);
	}
	public void TapIniciar()
	{
		StartCoroutine (IEIniciar());
	}
	private IEnumerator IEIniciar()
	{
		yield return gameManager.sceneController.On ();
		gameManager.condicionesManager.Init ();
		gameManager.inventarioManager.CargarInventario ();
		gameManager.touchGamePadManager.ActivarDesactivarGamePad (MenuInicio.activeInHierarchy);
		Interfaz.SetActive (MenuInicio.activeInHierarchy);
		MenuInicio.SetActive (!MenuInicio.activeInHierarchy);
	}
	public void TapOpciones()
	{
		MenuOpciones.SetActive (!MenuOpciones.activeInHierarchy);
	}
	public void TapIdioma()
	{
		MenuIdioma.SetActive (!MenuIdioma.activeInHierarchy);
	}
	public void TapResolucion()
	{
		MenuResolucion.SetActive(!MenuResolucion.activeInHierarchy);
	}
	public void TapInventario()
	{
		gameManager.touchGamePadManager.ActivarDesactivarRightGamePad (MenuInventario.activeInHierarchy);
		Interfaz.SetActive (!Interfaz.activeInHierarchy);
		MenuInventario.SetActive (!MenuInventario.activeInHierarchy);
		if (!MenuInventario.activeInHierarchy) 
			gameManager.inventarioManager.GuardarInventario ();
		if (MenuPickup.activeInHierarchy) 
			TapPickup();
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
		if (!MenuExaminar.activeInHierarchy)
			GuardarTodaInterfazAbierta ();
		AbrirCerrarInterfaz (MenuExaminar.activeInHierarchy);
		gameManager.touchGamePadManager.ActivarDesactivarGamePad (false);
		Interfaz.SetActive (false);
		if (MenuExaminar.activeInHierarchy)
			gameManager.touchGamePadManager.ActivarDesactivarLeftGamePad (!MenuPickup.activeInHierarchy);
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
		Time.timeScale = 1f;
		//Guardar Posicion relativa del player.
		loaderManager.SavePlayerPosition();
		loaderManager.Guardar ();

		StartCoroutine (IEQuitScene());
	}
	public void TapCerrarAplicacion()
	{
		Application.Quit ();
	}
	private IEnumerator IEQuitScene()
	{
		yield return gameManager.sceneController.Quit ();

		gameManager.inventarioManager.CargarInventario ();
		gameManager.touchGamePadManager.ActivarDesactivarGamePad (MenuInicio.activeInHierarchy);
		Interfaz.SetActive (MenuInicio.activeInHierarchy);
		MenuInicio.SetActive (!MenuInicio.activeInHierarchy);
		MenuPausa.SetActive (!MenuPausa.activeInHierarchy);

		StartCoroutine (gameManager.sceneController.Fade (0f));
	}
	void OnApplicationQuit()
	{
		if (gameManager.LocalPlayer != null) 
		{
			loaderManager.SavePlayerPosition ();
			loaderManager.Guardar ();
		}
		Debug.Log ("Cerrando aplicación!!");
	}
	private void OnApplicationPause(bool pause)
	{
		if (pause == true)
		{
			if (gameManager.LocalPlayer != null)
			{
				loaderManager.SavePlayerPosition();
				loaderManager.Guardar();
			}
		}	
	}
}

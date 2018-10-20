using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	#region Singleton
	public static GameManager instance;
	void Awake ()
	{
		if (instance != null)
			return;
		instance = this;
	}
	#endregion

	//EVENTOS
	public event System.Action<PlayerManager> OnLocalPlayerJoined;
	public event System.Action<CameraManager> OnActualCameraManagerJoined;
	public event System.Action<LevelManager> OnActualLevelManagerJoined;

	//VARIABLES PÚBLICAS
	[HideInInspector]public InputManager InputManager;
	[HideInInspector]public TouchGamePadManager touchGamePadManager;

	//INSTANCIADORES
	private  PlayerManager m_Player;
	public PlayerManager LocalPlayer
	{
		get
		{
			return m_Player;
		}
		set
		{
			m_Player = value;
			if (OnLocalPlayerJoined != null)
				OnLocalPlayerJoined (m_Player);
		}
	}

	private CameraManager m_Cam;
	public CameraManager ActualCameraManager
	{
		get
		{
			return m_Cam;
		}
		set
		{
			m_Cam = value;
			if (OnActualCameraManagerJoined != null)
				OnActualCameraManagerJoined (m_Cam);
		}
	}

	private LevelManager m_Lvl;
	public LevelManager ActualLevelManager
	{
		get
		{
			return m_Lvl;
		}
		set
		{
			m_Lvl = value;
			if (OnActualLevelManagerJoined != null)
				OnActualLevelManagerJoined (m_Lvl);
		}
	}

	private CanvasManager m_canvasManager;
	public CanvasManager canvasManager
	{
		get
		{
			if (m_canvasManager == null)
				m_canvasManager = GameObject.Find ("CanvasManager").GetComponent<CanvasManager> ();
			return m_canvasManager;
		}
	}

	private LocalizationManager m_localizationManager;
	public LocalizationManager localizationManager
	{
		get
		{
			if (m_localizationManager == null)
				m_localizationManager = GameObject.Find ("LocalizationManager").GetComponent<LocalizationManager> ();
			return m_localizationManager;
		}
	}

	private InventarioManager m_inventarioManager;
	public InventarioManager inventarioManager
	{
		get
		{
			if (m_inventarioManager == null)
				m_inventarioManager = GameObject.Find ("InventarioManager").GetComponent<InventarioManager> ();
			return m_inventarioManager;
		}
	}

	private SceneController m_sceneController;
	public SceneController sceneController
	{
		get
		{
			if (m_sceneController == null)
				m_sceneController = transform.GetComponent<SceneController> ();
			return m_sceneController;
		}
	}

	//**************************************************

	void Start () 
	{
		Debug.Log ("Start GameManager");
		sceneController.faderCanvasGroup.alpha = 0f;
		Init ();
		canvasManager.Init();
		localizationManager.Init();
		EquipmentManager.Singleton.Init ();
		inventarioManager.Init ();
		//canvasManager.TapIniciar ();
		if (!LoaderManager.Singleton.Cargar ())
			return;
	}

	void Init()
	{
		InputManager = gameObject.GetComponent<InputManager> ();
		touchGamePadManager = canvasManager.touchGamePadManager;
	}

	void Update () 
	{
		float delta = Time.deltaTime;
		InputManager.Tick ();

		GameObject goPlayer = GameObject.Find ("Player");
		
		if (LocalPlayer != null && goPlayer != null) {
			//Debug.Log ("Tick LocalPlayer");
			LocalPlayer.Tick 
			(
				delta, 
				InputManager.d_a, InputManager.d_b, InputManager.d_x, InputManager.d_y, 
				InputManager.u_a, InputManager.u_b, InputManager.u_x, InputManager.u_y
			);
		}
	}

	void FixedUpdate()
	{
		float fixedDelta = Time.fixedDeltaTime;
		InputManager.FixedTick ();

		GameObject goPlayer = GameObject.Find ("Player");

		if (LocalPlayer != null && goPlayer != null) {
			//Debug.Log ("fTick LocalPlayer");
			LocalPlayer.FixedTick 
			(
				fixedDelta, 
				InputManager.AxisL, 
				InputManager.AxisR, 
				ActualCameraManager.Cameras [ActualCameraManager.ActiveCamera]
			);
		}
	}
		
}

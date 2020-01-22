using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGameManager : MonoBehaviour
{
    public static TestingGameManager instance;
    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        Debug.Log("Awake TestingGameManager!!");
    }
    //EVENTOS
    public event System.Action<PlayerManager> OnLocalPlayerJoined;

    //VARIABLES PÚBLICAS
    [HideInInspector] public InputManager InputManager;
    [HideInInspector] public TouchGamePadManager TouchGamePadManager;
    [HideInInspector] public GameObject camera;

    //INSTANCIADORES
    private PlayerManager m_Player;
    public PlayerManager LocalPlayer
    {
        get
        {
            return m_Player;
        }
        set
        {
            m_Player = value;
            OnLocalPlayerJoined?.Invoke(m_Player);
        }
    }
    private TestingCanvasMamager m_TestingCanvasManager;
    public TestingCanvasMamager testingCanvasManager
    {
        get
        {
            if (m_TestingCanvasManager == null)
                m_TestingCanvasManager = GameObject.Find("TestingCanvasManager").GetComponent<TestingCanvasMamager>();
            return m_TestingCanvasManager;
        }
    }

    //**************************************************************

    void Start()
    {
        Debug.Log("Start TestingGameManager!!");
        Init();
    }
    void Init()
    {
        InputManager = gameObject.GetComponent<InputManager>();
        TouchGamePadManager = testingCanvasManager.touchGamePadManager;
        camera = GameObject.Find("Camera");
        LocalPlayer.Init();

    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        InputManager.Tick();

        GameObject goPlayer = GameObject.Find("Player");

        if(LocalPlayer!=null && goPlayer != null)
        {
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
        InputManager.FixedTick();

        GameObject goPlayer = GameObject.Find("Player");

        if (LocalPlayer != null && goPlayer != null)
        {
            LocalPlayer.FixedTick
            (
                fixedDelta,
                InputManager.AxisL,
                InputManager.AxisR,
                camera
            );
        }
    }
}

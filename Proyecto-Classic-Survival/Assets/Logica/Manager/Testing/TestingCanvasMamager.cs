using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingCanvasMamager : MonoBehaviour
{
    //VARIABLES PRIVADAS
    private TestingGameManager gameManager;

    //INSTANCIACIONES
    private TouchGamePadManager m_TouchGamePadManager;
    public TouchGamePadManager touchGamePadManager
    {
        get
        {
            if (m_TouchGamePadManager == null)
                m_TouchGamePadManager = gameObject.GetComponent<TouchGamePadManager>();
            return m_TouchGamePadManager;
        }
    }

    void Init()
    {
        Debug.Log("inicia TestingCanvasManager!!");
        gameManager = TestingGameManager.instance;
    }
}

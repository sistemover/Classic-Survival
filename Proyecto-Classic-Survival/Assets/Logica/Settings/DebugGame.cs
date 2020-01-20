using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugGame : MonoBehaviour
{
    #region Singleton
    public static DebugGame instance;
    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion

    
    public Text DebugText;
    public Vector3 JoystickStartPosition;
    public float MovementRange;

    int framecount;
    float Deltatime;
    float UpdateRate = 2.0f;
    double fps;
    string m_debug;

    
    void Update()
    {       
        framecount++;
        Deltatime += Time.deltaTime;

        if (Deltatime > 1 / UpdateRate)
        {
            fps = System.Math.Round(framecount / Deltatime, 1);

            m_debug = fps.ToString() + " FPS " + " || " + Screen.currentResolution.width + "X" + Screen.currentResolution.height + " || " + JoystickStartPosition + "||" + MovementRange;
            DebugText.text = m_debug;

            framecount = 0;
            Deltatime -= 1 / UpdateRate;
        }        
    }

    /*
    private void OnApplicationPause(bool pause)
    {
        m_debug = m_debug + " || " + pause;
        debugText.text = m_debug;
    }*/
}

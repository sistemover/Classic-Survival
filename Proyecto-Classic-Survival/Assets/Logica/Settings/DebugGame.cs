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

    
    public Text debugText;
    public Vector3 JoystickStartPosition;

    int framecount;
    float Deltatime;
    float UpdateRate = 2.0f;
    double fps;
    
    void Update()
    {       
        framecount++;
        Deltatime += Time.deltaTime;

        if (Deltatime > 1 / UpdateRate)
        {
            fps = System.Math.Round(framecount / Deltatime, 1);

            debugText.text = fps.ToString() + " FPS " + " || " + Screen.currentResolution.width + "X" + Screen.currentResolution.height + " || " + JoystickStartPosition;

            framecount = 0;
            Deltatime -= 1 / UpdateRate;
        }        
    }
}

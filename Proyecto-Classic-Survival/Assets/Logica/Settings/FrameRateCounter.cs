using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateCounter : MonoBehaviour
{
    public int framecount;
    float Deltatime;
    public float UpdateRate = 2.0f;
    double fps;
    public Text fpstext;
    void Update()
    {
        framecount++;
        Deltatime += Time.deltaTime;

        if (Deltatime > 1 / UpdateRate)
        {
            fps = System.Math.Round(framecount / Deltatime, 1);
            fpstext.text = fps.ToString() + " FPS";
            framecount = 0;
            Deltatime -= 1 / UpdateRate;
        }
    }
}

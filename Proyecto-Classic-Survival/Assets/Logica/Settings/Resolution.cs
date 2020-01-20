using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    int DefaultResolutionHeight;
    int DefaultResolutionWidth;

    GameManager gameManager;

    public bool[] CurrentResolution;

    void Start()
    {
        gameManager = GameManager.instance;
        DefaultResolutionWidth = Screen.currentResolution.width;
        DefaultResolutionHeight = Screen.currentResolution.height;
        CurrentResolution = new bool[5];
        setResolution(0);
    }

    void setResolution(int i)
    {
        for (int e = 0; e < CurrentResolution.Length; e++)
        {
            CurrentResolution[e] = false;
            if (i == e)
                CurrentResolution[e] = true;
        }
    }
    void SetResolutionScreen(int h, int v) 
    {
        Screen.SetResolution(h,v,true);
    }
    public void setDefault()
    {
        Screen.SetResolution(DefaultResolutionWidth,DefaultResolutionHeight,true);
        setResolution(0);
    }
    public void SetRes1080()
    {
        SetResolutionScreen(CalcNewWidth(1080),1080);
        setResolution(1);
    }
    public void setRes720() 
    {
        SetResolutionScreen(CalcNewWidth(720), 720);
        setResolution(2);
    }
    public void setRes480() 
    {
        SetResolutionScreen(CalcNewWidth(480),480);
        setResolution(3);
    }

    public void setRes360()
    {
        SetResolutionScreen(CalcNewWidth(360), 360);
        setResolution(5);
    }
    public int CalcNewWidth(int baseResolution)
    {
        int newWidth = (DefaultResolutionWidth* baseResolution) / DefaultResolutionHeight;
        return newWidth;
    }
}

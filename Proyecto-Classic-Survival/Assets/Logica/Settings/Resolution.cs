using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    int OriginalResolutionHeight;
    int originalResolutionWidth;
    // Start is called before the first frame update
    void Start()
    {
        originalResolutionWidth = Screen.currentResolution.width;
        OriginalResolutionHeight = Screen.currentResolution.height;
    }

    void SetResolutionScreen(int h, int v) 
    {
        Screen.SetResolution(h,v,true);
    }
    public void SetRes1080()
    {
        SetResolutionScreen(CalcNewWidth(1080),1080);
    }
    public void setRes720() 
    {
        SetResolutionScreen(CalcNewWidth(720), 720);
    }
    public void setRes480() 
    {
        SetResolutionScreen(CalcNewWidth(480),480);
    }
    public int CalcNewWidth(int baseResolution)
    {
        int newWidth = (originalResolutionWidth* baseResolution) / OriginalResolutionHeight;
        return newWidth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingLevelManager : MonoBehaviour
{
    TestingGameManager gameManager;
    void Start()
    {
        gameManager = TestingGameManager.instance;
        gameManager.LocalPlayer.Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

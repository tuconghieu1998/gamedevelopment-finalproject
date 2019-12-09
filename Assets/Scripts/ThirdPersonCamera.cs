using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    Player localPlayer;
    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined; 
    }

    private void HandleOnLocalPlayerJoined(Player player)
    {
        localPlayer = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

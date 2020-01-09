using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    GameObject gameManager;

    void Start()
    {
        gameManager = GameManager.Instance.gameObject;
        DontDestroyOnLoad(gameManager);
    }


    void Update()
    {
        
    }
}

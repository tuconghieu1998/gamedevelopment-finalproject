using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    InputController inputController;

    // Start is called before the first frame update
    void Start()
    {
        inputController = GameManager.Instance.InputController;
    }

    // Update is called once per frame
    void Update()
    {
        print("Horizontal " + inputController.Horizontal);
        print("Mouse " + inputController.MouseInput);
    }
}

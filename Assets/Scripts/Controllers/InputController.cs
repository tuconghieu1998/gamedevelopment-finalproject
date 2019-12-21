using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Reload;
    public bool isWalking;
    public bool isSprinting;
    public bool isCrouched;
    public bool MouseWheelUp;
    public bool MouseWheelDown;

    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Reload = Input.GetKey(KeyCode.R);
        isWalking = Input.GetKey(KeyCode.LeftAlt);
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        isCrouched = Input.GetKey(KeyCode.Space);
        MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
    }
}

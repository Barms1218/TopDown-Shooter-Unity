using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override float RetrieveHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public override float RetrieveVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }

    public override bool RetrieveShootInput()
    {
        return Input.GetMouseButtonDown(0);
    }
    public override bool MouseHeldDown()
    {
        return Input.GetMouseButtonDown(1);
    }
    public override bool RetrieveReloadInput()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
     public override bool RetrieveReloadInput()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
    public override bool RetrieveShootInput()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}

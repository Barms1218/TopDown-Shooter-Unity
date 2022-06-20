using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    public override bool RetrieveShootInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    public override bool RetrieveReloadInput()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    public override float RetrieveHorizontalInput()
    {
        return 1f;
    }

    public override float RetrieveVerticalInput()
    {
        return 1f;
    }

    public override bool RetrieveShootInput()
    {
        return true; ;
    }

    public override bool RetrieveReloadInput()
    {
        return true;
    }

    public override bool MouseHeldDown()
    {
        return Input.GetMouseButtonDown(0);
    }
}

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
}

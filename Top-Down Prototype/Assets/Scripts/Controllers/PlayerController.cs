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
}

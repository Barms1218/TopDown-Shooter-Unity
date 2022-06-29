using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool ReloadInput() => Input.GetKeyDown(KeyCode.R);
    public override bool FireInput() => Input.GetMouseButton(0);
    public override bool InteractInput() => Input.GetKeyDown(KeyCode.E);
    public override bool SpecialAttackInput() => Input.GetKeyDown(KeyCode.F);
    public override bool DashInput() => Input.GetKeyDown(KeyCode.Space);
}

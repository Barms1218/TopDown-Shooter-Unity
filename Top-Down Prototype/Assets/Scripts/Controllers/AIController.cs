using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    public override bool ReloadInput() => true;
    public override bool FireInput() => true;
    public override bool InteractInput() => Input.GetKeyDown(KeyCode.E);
    public override bool SpecialAttackInput() => true;
    public override bool DashInput() => true;
}

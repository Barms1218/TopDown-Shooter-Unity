using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    public override bool Reload() => true;
    public override bool Fire() => true;
    public override bool Interact() => Input.GetKeyDown(KeyCode.E);
    public override bool SpecialAttack() => true;
    public override float HorizontalInput() => 1f;
    public override float VerticalInput() => 1f;
}

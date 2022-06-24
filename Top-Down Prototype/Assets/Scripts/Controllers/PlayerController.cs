using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool Reload() => Input.GetKeyDown(KeyCode.R);
    public override bool Fire() => Input.GetMouseButton(0);
    public override bool Interact() => Input.GetKeyDown(KeyCode.E);
    public override bool SpecialAttack() => Input.GetKeyDown(KeyCode.F);
    public override float HorizontalInput() => Input.GetAxis("Horizontal");
    public override float VerticalInput() => Input.GetAxis("Vertical");
}

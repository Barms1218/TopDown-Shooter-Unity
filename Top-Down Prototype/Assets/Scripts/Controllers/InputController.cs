using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract bool Fire();
    public abstract bool Reload();
    public abstract bool SpecialAttack();
    public abstract bool Interact();
    public abstract float HorizontalInput();
    public abstract float VerticalInput();
}

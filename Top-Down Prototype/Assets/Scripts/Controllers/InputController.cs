using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract bool FireInput();
    public abstract bool ReloadInput();
    public abstract bool SpecialAttackInput();
    public abstract bool InteractInput();
    public abstract bool DashInput();
}

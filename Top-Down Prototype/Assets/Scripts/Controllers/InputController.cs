using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract bool RetrieveShootInput();
    public abstract bool RetrieveReloadInput();
}

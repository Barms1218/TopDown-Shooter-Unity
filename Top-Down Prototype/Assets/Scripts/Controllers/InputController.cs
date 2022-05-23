using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveHorizontalInput();
    public abstract float RetrieveVerticalInput();

    public abstract bool RetrieveShootInput();

    public abstract bool RetrieveReloadInput();
}

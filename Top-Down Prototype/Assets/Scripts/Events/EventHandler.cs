using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler
{
    public delegate void OnDiedDelegate();
    public static event OnDiedDelegate OnDiedTrigger;

    public void OnDied()
    {
        if (OnDiedTrigger != null)
        {
            OnDiedTrigger();
        }
    }
}

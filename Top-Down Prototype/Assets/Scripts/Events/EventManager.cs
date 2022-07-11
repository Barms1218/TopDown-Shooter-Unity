using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static List<Health> onDiedInvokers = new List<Health>();
    static List<UnityAction<GameObject>> healthListeners = new List<UnityAction<GameObject>>();

    public static void AddOnDiedEventInvoker(Health invoker)
    {
        onDiedInvokers.Add(invoker);
        foreach (UnityAction<GameObject> _listener in healthListeners)
        {
            invoker.AddOnDiedEventListener(_listener);
        }
    }

    public static void AddHealthListener(UnityAction<GameObject> _listener)
    {
        healthListeners.Add(_listener);
        foreach (Health invoker in onDiedInvokers)
        {
            invoker.AddOnDiedEventListener(_listener);
        }
    }

    public static void RemoveInvoker(Health invoker)
    {
        onDiedInvokers.Remove(invoker);
    }

    public static void RemoveListener(UnityAction<GameObject> listener)
    {
        healthListeners.Remove(listener);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    private static List<Health> onDiedInvokers = new List<Health>();
    private static List<UnityAction<GameObject>> healthListeners = new List<UnityAction<GameObject>>();

    private static List<EnemyDeath> pointsAddedInvokers = new List<EnemyDeath>();
    private static List<UnityAction<int>> pointsAddedListeners = new List<UnityAction<int>>();

    private static List<PlayerWeaponHandler> setAmmoInvoker = new List<PlayerWeaponHandler>();
    private static List<UnityAction<int, int>> setAmmoListener = new List<UnityAction<int, int>>();


    #region Enemy Death Event
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

    public static void RemoveOnDiedInvoker(Health invoker)
    {
        onDiedInvokers.Remove(invoker);
    }

    public static void RemoveOnDiedListener(UnityAction<GameObject> listener)
    {
        healthListeners.Remove(listener);
    }

    #endregion

    #region Points Added Event

    public static void AddPointsAddedInvoker(EnemyDeath invoker)
    {
        pointsAddedInvokers.Add(invoker);
        foreach (UnityAction<int> _listener in pointsAddedListeners)
        {
            invoker.AddPointsAddedListener(_listener);
        }
    }

    public static void AddPointsAddedListener(UnityAction<int> _listener)
    {
        pointsAddedListeners.Add(_listener);
        foreach (EnemyDeath invoker in pointsAddedInvokers)
        {
            invoker.AddPointsAddedListener(_listener);
        }
    }

    public static void RemovePointsAddedInvoker(EnemyDeath invoker)
    {
        pointsAddedInvokers.Remove(invoker);
    }

    #endregion


}

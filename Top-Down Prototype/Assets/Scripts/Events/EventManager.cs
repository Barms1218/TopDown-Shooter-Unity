using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    //private static List<Health> onDiedInvokers = new List<Health>();
    //private static List<UnityAction<GameObject>> onDiedListeners = new List<UnityAction<GameObject>>();
    //private static List<EnemyDeath> pointsAddedInvokers = new List<EnemyDeath>();
    //private static List<UnityAction<int>> pointsAddedListeners = new List<UnityAction<int>>();

    //public static void AddOnDiedEventInvoker(Health invoker)
    //{
    //    onDiedInvokers.Add(invoker);
    //    foreach (UnityAction<GameObject> _listener in onDiedListeners)
    //    {
    //        invoker.AddOnDiedEventListener(_listener);
    //    }
    //}

    //public static void AddOnDiedEventListener(UnityAction<GameObject> _listener)
    //{
    //    onDiedListeners.Add(_listener);
    //    foreach (Health invoker in onDiedInvokers)
    //    {
    //        invoker.AddOnDiedEventListener(_listener);
    //    }
    //}

    //#region Points Added Event

    //public static void AddPointsAddedInvoker(EnemyDeath invoker)
    //{
    //    pointsAddedInvokers.Add(invoker);
    //    foreach (UnityAction<int> _listener in pointsAddedListeners)
    //    {
    //        invoker.AddPointsAddedListener(_listener);
    //    }
    //}

    //public static void AddPointsAddedListener(UnityAction<int> _listener)
    //{
    //    pointsAddedListeners.Add(_listener);
    //    foreach (EnemyDeath invoker in pointsAddedInvokers)
    //    {
    //        invoker.AddPointsAddedListener(_listener);
    //    }
    //}

    //public static void RemovePointsAddedInvoker(EnemyDeath invoker)
    //{
    //    pointsAddedInvokers.Remove(invoker);
    //}

    //#endregion


}

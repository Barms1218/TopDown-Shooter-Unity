using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent _gameEvent;
    [SerializeField] UnityAction _unityEvent;

    private void Awake() => _gameEvent.Register(this);
    private void OnDestroy() => _gameEvent.DeRegister(this);
    public void RaiseEvent() => _unityEvent.Invoke();
}

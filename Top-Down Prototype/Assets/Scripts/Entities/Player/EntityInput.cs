using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityInput : MonoBehaviour
{
    [SerializeField] InputController input; 
    public bool Reload { get; private set; }
    public bool Fire { get; private set; }
    public bool SpecialAttack { get; private set; }
    public bool Dash { get; private set; }
    public static UnityAction OnReload;
    public static UnityAction OnSpecialAttack;
    public static UnityAction OnFire;
    public static UnityAction OnDash;


    private void Update()
    {
        Reload = input.ReloadInput();
        SpecialAttack = input.SpecialAttackInput();
        Fire = input.FireInput();
        Dash = input.DashInput();
        if (Fire)
        {
            OnFire?.Invoke();
        }
        if (Reload)
        {
            OnReload?.Invoke();
        }
        if (SpecialAttack)
        {
            OnSpecialAttack?.Invoke();
        }
    }
}

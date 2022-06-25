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
    public bool[] SwapWeapon { get; private set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public static UnityAction OnReload;
    public static UnityAction OnInteract;
    public static UnityAction OnSpecialAttack;
    public static UnityAction OnFire;


    private void Update()
    {
        HorizontalInput = input.HorizontalInput();
        VerticalInput = input.VerticalInput();
        Reload = input.Reload();
        SpecialAttack = input.SpecialAttack();
        Fire = input.Fire();
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

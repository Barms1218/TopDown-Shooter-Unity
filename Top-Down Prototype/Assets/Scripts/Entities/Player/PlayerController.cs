using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : Controller
{
    public static PlayerController player;
    [SerializeField]
    GameObject gun;
    private PlayerActions actions;
    [SerializeField]
    PlayerWeaponHandler weaponHandler;
    [SerializeField]
    WeaponSwap weaponSwap;

    Weapon currentWeapon;

    Coroutine fireCoroutine;

    // Vector Input Actions
    InputAction move;

    // delegates
    public UnityAction<Vector2> moveDelegate;
    public UnityAction dashDelegate;
    public UnityAction<IEnumerator> rapidFireEvent;

    protected override void Awake()
    {
        base.Awake();
        player = this;

        actions = new PlayerActions();
        move = actions.PlayerControls.Movement;

        // inputs for player weapon
        actions.PlayerControls.Shoot.started += _ => StartFiring();
        actions.PlayerControls.Shoot.canceled += _ => StopFiring();
        actions.PlayerControls.Reload.started += _ => weaponHandler.Reload();

        //Dash input
        actions.PlayerControls.Dash.started += _ => Dash();

        // Inputs for weapon swap
        actions.PlayerControls.EquipWeapon1.started += _ => weaponSwap.TryEquipWeaponOne();
        actions.PlayerControls.EquipWeapon2.started += _ => weaponSwap.TryEquipWeaponTwo();
        actions.PlayerControls.EquipWeapon3.started += _ => weaponSwap.TryEquipWeaponThree();

        
    }

    protected override void FixedUpdate()
    {
        moveDelegate?.Invoke(move.ReadValue<Vector2>());
    }

    void Dash()
    {
        dashDelegate?.Invoke();
    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(weaponHandler.RapidFire());
        //rapidFireEvent?.Invoke(weaponHandler.RapidFire());
    }

    void StopFiring()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

}

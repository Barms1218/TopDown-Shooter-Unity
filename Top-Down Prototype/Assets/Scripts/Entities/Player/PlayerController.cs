using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    private PlayerActions actions;
    [SerializeField] PlayerWeaponHandler weaponHandler;
    [SerializeField] PlayerMovement movement;
    [SerializeField] WeaponSwap weaponSwap;
    [SerializeField] SetTheCursor setTheCursor;

    Coroutine fireCoroutine;

    InputAction move;
    InputAction aim;

    private void Awake()
    {
        player = this;

        actions = new PlayerActions();
        move = actions.PlayerControls.Movement;
        aim = actions.PlayerControls.Aim;

        // inputs for player weapon
        actions.PlayerControls.Shoot.started += _ => StartFiring();
        actions.PlayerControls.Shoot.canceled += _ => StopFiring();
        actions.PlayerControls.Reload.started += _ => weaponHandler.Reload();

        //Dash input
        actions.PlayerControls.Dash.started += _ => movement.Dash();

        // Inputs for weapon swap
        actions.PlayerControls.EquipWeapon1.started += weaponSwap.TryEquipWeaponOne;
        actions.PlayerControls.EquipWeapon2.started += weaponSwap.TryEquipWeaponTwo;
        actions.PlayerControls.EquipWeapon3.started += weaponSwap.TryEquipWeaponThree;
    }
    private void Update()
    {
        setTheCursor.ChangeCursorPosition(aim.ReadValue<Vector2>());
        weaponHandler.Aim(aim.ReadValue<Vector2>());
    }
    private void FixedUpdate()
    {
        movement.Movement(move.ReadValue<Vector2>());

    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(weaponHandler.RapidFire());
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

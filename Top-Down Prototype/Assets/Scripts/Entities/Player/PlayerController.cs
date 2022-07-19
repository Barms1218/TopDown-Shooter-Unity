using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerActions actions;
    [SerializeField] PlayerWeaponHandler weaponHandler;
    [SerializeField] PlayerMovement movement;
    [SerializeField] WeaponSwap weaponSwap;
    [SerializeField] SetTheCursor setTheCursor;

    InputAction move;
    InputAction shoot;
    InputAction aim;
    InputAction equipWeaponOne;

    private void Awake()
    {
        actions = new PlayerActions();
        move = actions.PlayerControls.Movement;
        aim = actions.PlayerControls.Aim;

        actions.PlayerControls.Shoot.started += weaponHandler.Shoot;

        // input for dash ability
        actions.PlayerControls.Dash.started += movement.OnDash;

        //Swap the player's weapon
        actions.PlayerControls.EquipWeapon1.started += weaponSwap.TryEquipWeaponOne;
        actions.PlayerControls.EquipWeapon2.started += weaponSwap.TryEquipWeaponTwo;
        actions.PlayerControls.EquipWeapon3.started += weaponSwap.TryEquipWeaponThree;
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        movement.Movement(move.ReadValue<Vector2>());
        setTheCursor.ChangeCursorPosition(aim.ReadValue<Vector2>());
        weaponHandler.Aim(aim.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Move))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    private PlayerActions actions;
    PlayerShoot shooter;
    PlayerHolster holster;
    Move playerMove;

    Coroutine fireCoroutine;

    // Vector Input Actions
    InputAction move;

    // delegates
    public UnityAction dashDelegate;
    public UnityAction<IEnumerator> rapidFireEvent;

    private void Awake()
    {
        holster = GetComponent<PlayerHolster>();
        shooter = GetComponent<PlayerShoot>();
        playerMove = GetComponent<Move>();
        player = this;

        actions = new PlayerActions();
        move = actions.PlayerControls.Movement;

        // inputs for player weapon
        actions.PlayerControls.Shoot.started += _ => StartFiring();
        actions.PlayerControls.Shoot.canceled += _ => StopFiring();
        actions.PlayerControls.Reload.started += _ => shooter.Reload();

        //Dash input
        //actions.PlayerControls.Dash.started += _ => Dash();

        // Inputs for weapon swap
        actions.PlayerControls.EquipWeapon1.started += _ => holster.TryEquipWeaponOne();
        actions.PlayerControls.EquipWeapon2.started += _ => holster.TryEquipWeaponTwo();
        actions.PlayerControls.EquipWeapon3.started += _ => holster.TryEquipWeaponThree();
    }

    private void FixedUpdate()
    {
        playerMove.MoveObject(move.ReadValue<Vector2>());
    }

    private void Dash()
    {
        //dashDelegate?.Invoke();
    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(shooter.RapidFire());
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Move))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController playerInstance;
    [SerializeField] float playerSpeed;
    private PlayerControls inputActions;
    Shooter shooter;
    WeaponHolder holder;
    Move playerMove;
    private bool canDash = true;

    Coroutine fireCoroutine;

    // Vector Input Actions
    InputAction move;

    public static PlayerController PlayerInstance
    {
        get
        {
            if (playerInstance == null)
                Debug.Log("There's no player");

            return playerInstance;
        }
    }

    // delegates
    public UnityAction dashDelegate;

    private void Awake()
    {
        holder = GetComponentInChildren<WeaponHolder>();
        shooter = GetComponent<Shooter>();
        playerMove = GetComponent<Move>();
        playerInstance = this;

        inputActions = new PlayerControls();
        move = inputActions.Player.Movement;

        // inputs for player weapon
        inputActions.Player.Shoot.started += _ => StartFiring();
        inputActions.Player.Shoot.canceled += _ => StopFiring();
        inputActions.Player.Reload.started += _ => shooter.Reload();

        //Dash input
        inputActions.Player.Dash.started += _ => Dash();

        // Inputs for weapon swap
        inputActions.Player.EquipWeapon1.started += _ => holder.TryEquipWeaponOne();
        inputActions.Player.EquipWeapon2.started += _ => holder.TryEquipWeaponTwo();
        inputActions.Player.EquipWeapon3.started += _ => holder.TryEquipWeaponThree();
    }

    private void FixedUpdate()
    {
        playerMove.MoveObject(move.ReadValue<Vector2>(), playerSpeed);
    }

    private void Dash()
    {
        if (canDash)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        playerSpeed *= 2;
        yield return new WaitForSeconds(0.3f);
        canDash = false;
        playerSpeed /= 2;

        yield return new WaitForSeconds(2f);
        canDash = true;
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
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

}

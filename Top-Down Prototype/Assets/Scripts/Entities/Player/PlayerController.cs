using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    private PlayerActions actions;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private Animator _animator;
    [SerializeField] PlayerWeaponHandler weaponHandler;
    [SerializeField] PlayerMovement movement;
    [SerializeField] WeaponSwap weaponSwap;
    [SerializeField] private bool canMove = true;
    [SerializeField] GameObject childObject;

    Coroutine fireCoroutine;

    // Vector Input Actions
    InputAction move;

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    public Rigidbody2D Rigidbody2D => rb2d;
    public Collider2D Collider => _collider2D;
    public Animator Animator => _animator;
    public GameObject Child => childObject;

    private void Awake()
    {
        player = this;

        actions = new PlayerActions();
        move = actions.PlayerControls.Movement;

        // inputs for player weapon
        actions.PlayerControls.Shoot.started += _ => StartFiring();
        actions.PlayerControls.Shoot.canceled += _ => StopFiring();
        actions.PlayerControls.Reload.started += _ => weaponHandler.Reload();

        //Dash input
        actions.PlayerControls.Dash.started += _ => movement.Dash();

        // Inputs for weapon swap
        actions.PlayerControls.EquipWeapon1.started += _ => weaponSwap.TryEquipWeaponOne();
        actions.PlayerControls.EquipWeapon2.started += _ => weaponSwap.TryEquipWeaponTwo();
        actions.PlayerControls.EquipWeapon3.started += _ => weaponSwap.TryEquipWeaponThree();

        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            movement.Movement(move.ReadValue<Vector2>());
        }

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

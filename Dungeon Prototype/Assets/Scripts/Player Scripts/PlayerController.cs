using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMaps actions;
    [SerializeField] private Move move;
    private Transform cameraTransform;
    InputAction movement;
    InputAction jump;
    Coroutine sprintRoutine;

    private void Awake()
    {
        actions = new PlayerMaps();
        movement = actions.Player.Movement;
        jump = actions.Player.Jump;
        //move = GetComponent<Move>();
        cameraTransform = Camera.main.transform;

        jump.started += _ => JumpTriggered();

        actions.Player.Dodge.started += _ => Dodge();

        actions.Player.Sprint.started += _ => StartSprinting();
        actions.Player.Sprint.canceled += _ => StopSprinting();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        var input = movement.ReadValue<Vector2>();
        Vector3 moveInput;
        if (move.IsGrounded)
        {
            moveInput = new Vector3(input.x, 0f, input.y);
        }
        else
        {
            moveInput = new Vector3(0f, 0f, input.y);
        }
        var movePlayer = moveInput.z * cameraTransform.forward +
            cameraTransform.right * moveInput.x;
        movePlayer.y = 0;
        move.MoveObject(movePlayer);

    }

    private void Dodge()
    {
        if (move.IsGrounded && move.CanDash)
        {
            StartCoroutine(move.Dodge());
        }
    }

    private void StartSprinting()
    {
        sprintRoutine = StartCoroutine(move.Sprint());
    }

    private void StopSprinting()
    {
        StopCoroutine(sprintRoutine);
    }

    private void JumpTriggered()
    {
        if (move.IsGrounded)
        {
            move.Jump();
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

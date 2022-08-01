using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetTheCursor : MonoBehaviour
{
    public static SetTheCursor cursor;
    CursorActions inputActions;
    
    InputAction move;
    [SerializeField] float xValue;
    [SerializeField] float yValue;
    Transform playerTransform;
    
    // Start is called before the first frame update
    void Awake()
    {
        inputActions = new CursorActions();
        cursor = this;
        //Cursor.visible = false;
        move = inputActions.CursorAction.Move;
    }

    private void Start()
    {
        if (PlayerController.player != null)
        {
            playerTransform = PlayerController.player.transform;
        }
    }

    private void Update()
    {
        var inputValue = move.ReadValue<Vector2>();
        
        ChangeCursorPosition(inputValue);
    }

    public void ChangeCursorPosition(Vector2 newPosition)
    {
        var position = Camera.main.ScreenToWorldPoint(newPosition);
        if (playerTransform != null)
        {
            position.x = Mathf.Clamp(position.x, playerTransform.transform.position.x - xValue,
                playerTransform.transform.position.x + xValue);
            position.y = Mathf.Clamp(position.y, playerTransform.transform.position.y - yValue,
                playerTransform.transform.position.y + yValue);
            position.z = 0;
        }

        transform.position = position;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetTheCursor : MonoBehaviour
{

    [SerializeField] Texture2D cursorAim;
    [SerializeField] Texture2D cursorClicked;
    [SerializeField] float xValue;
    [SerializeField] float yValue;
    CursorActions inputActions;
    Transform playerTransform;
    InputAction move;


    // Properties
    public CursorActions CursorActions => inputActions;
    public Texture2D AimCursor => cursorAim;
    public Texture2D UICursor => cursorClicked;

    // Start is called before the first frame update
    void Awake()
    {
        inputActions = new CursorActions();
        ChangeCursor(cursorAim);
        move = inputActions.CursorAction.Move;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotSpot = new(cursorType.width / 2, cursorType.height / 2);

        Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto);
        //Cursor.lockState = CursorLockMode.Confined;
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

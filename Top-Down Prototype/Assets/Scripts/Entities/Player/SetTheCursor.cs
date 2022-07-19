using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetTheCursor : MonoBehaviour
{
    [SerializeField] float xValue;
    [SerializeField] float yValue;
    Vector2 position;
    Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void ChangeCursorPosition(Vector2 newPosition)
    {
        position = Camera.main.ScreenToWorldPoint(newPosition);
        position.x = Mathf.Clamp(position.x, playerTransform.transform.position.x - xValue,
            playerTransform.transform.position.x + xValue);
        position.y = Mathf.Clamp(position.y, playerTransform.transform.position.y - yValue,
            playerTransform.transform.position.y + yValue);

        transform.position = position;
    }
}

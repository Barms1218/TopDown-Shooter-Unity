using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetTheCursor : MonoBehaviour
{
    [SerializeField] float xValue;
    [SerializeField] float yValue;
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    Vector2 position;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void ChangeCursorPosition(InputAction.CallbackContext value)
    {
        position = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
        position.x = Mathf.Clamp(position.x, player.transform.position.x - xValue,
            player.transform.position.x + xValue);
        position.y = Mathf.Clamp(position.y, player.transform.position.y - yValue,
            player.transform.position.y + yValue);

        transform.position = position;
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    private void OnMouseDown()
    {
        spriteRenderer.sprite = sprites[1];
    }
}

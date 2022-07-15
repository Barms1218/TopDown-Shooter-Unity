using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTheCursor : MonoBehaviour
{
    [SerializeField] float xValue;
    [SerializeField] float yValue;
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    Vector3 mousePos;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z *= -1;
        mousePos.x = Mathf.Clamp(mousePos.x, player.transform.position.x - xValue,
            player.transform.position.x + xValue);
        mousePos.y = Mathf.Clamp(mousePos.y, player.transform.position.y - yValue,
            player.transform.position.y + yValue);

        transform.position = mousePos;
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

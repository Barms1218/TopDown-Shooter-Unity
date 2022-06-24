using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    EntityInput entityInput;
    // Start is called before the first frame update
    private void Awake()
    {
        entityInput = GetComponent<EntityInput>();
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector2(entityInput.HorizontalInput * player.transform.position.x,
        entityInput.VerticalInput * player.transform.position.y);
    }
}

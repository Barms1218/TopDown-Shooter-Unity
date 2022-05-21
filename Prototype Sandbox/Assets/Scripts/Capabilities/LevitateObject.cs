using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Levitate();

    }

    private void Levitate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;

        float rayDistance = 10f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance);

        Debug.DrawRay(transform.position, hit.point, Color.yellow);

    }
}

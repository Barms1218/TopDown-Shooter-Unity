using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTheCursor : MonoBehaviour
{
    Vector3 mousePos;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z *= -1;

        transform.position = mousePos;
    }
}

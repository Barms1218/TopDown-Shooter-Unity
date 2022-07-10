using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    GameObject player;
    Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z *= -1;
        Vector3 newPosition = player.transform.position - mousePosition;
        newPosition.x /= 2;
        newPosition.y /= 2;

        transform.position = newPosition;
    }
}

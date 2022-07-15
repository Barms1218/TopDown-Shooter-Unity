using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    Vector3 centerPosition;
    Vector3 mousePos;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        centerPosition = new Vector3((player.transform.position.x + mousePos.x) / 2,
            (player.transform.position.y + mousePos.y) / 2, 0);

        transform.position = centerPosition;
    }
}

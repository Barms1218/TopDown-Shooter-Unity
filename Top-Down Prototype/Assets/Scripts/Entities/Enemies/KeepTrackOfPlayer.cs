using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeepTrackOfPlayer : MonoBehaviour
{
    [SerializeField] Transform eyesTransform;
    [SerializeField] float sightRange = 10f;
    private Collider2D _collider;
    private GameObject _player;
    
    private bool seePlayer = false;
    public event UnityAction<bool> OnSightedPlayer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector2 _direction = _player.transform.position - transform.position;
        Color lineColor;

        lineColor = Color.red;

        RaycastHit2D hit2d = Physics2D.Raycast(_collider.bounds.center, _direction, sightRange);
        if (hit2d.collider != null && hit2d.collider.gameObject.CompareTag("Player"))
        {
            lineColor = Color.green;
            if (!seePlayer)
            {
                seePlayer = true;
                OnSightedPlayer?.Invoke(seePlayer);
                Debug.Log(seePlayer);
            }
        }
        else if (!hit2d.collider.gameObject.CompareTag("Enemy") && !hit2d.collider.gameObject.CompareTag("Player"))
        {
            if (seePlayer)
            {
                seePlayer = false;
                OnSightedPlayer?.Invoke(seePlayer);
                Debug.Log(seePlayer + " , " + hit2d.collider.gameObject.name);
            }
        }
        Debug.DrawRay(_collider.bounds.center, _direction, lineColor);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookForPlayer : MonoBehaviour
{
    [SerializeField] int sightRange = 7;
    [SerializeField] float maxDistance = 2f;
    [SerializeField] float moveAgainTime = 2f;
    [SerializeField] Collider2D _collider;
    Vector3 newPosition;
    GameObject _player;
    
    LayerMask detectionLayer;

    
    float moveTimer = 0;
    
    private bool facingRight = true;
    public static UnityAction SawPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerController.player.gameObject;
        detectionLayer = LayerMask.GetMask("Default", "Player");
        ChangePosition();
    }

    // Update is called once per frame
    void Update()
    {
        // enemy wanders around randomly
        if (!CanSeePlayer())
        {
            transform.position = Vector2.MoveTowards(
                transform.position, newPosition, 1f * Time.deltaTime);
            if (Time.time >= moveTimer)
            {
                ChangePosition();
                moveTimer = Time.time + moveAgainTime;
            }

            if (_player.transform.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (_player.transform.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }
    private bool CanSeePlayer()
    {
        var seePlayer = false;

        Color lineColor;

        lineColor = Color.red;

        if (_player != null)
        {

            Vector2 _direction = _player.transform.position - transform.position;
            RaycastHit2D hit2d = Physics2D.Raycast(_collider.bounds.center,
                _direction, sightRange, detectionLayer);
            if (hit2d.collider != null && hit2d.collider.gameObject.CompareTag("Player"))
            {
                lineColor = Color.green;
                if (!seePlayer)
                {
                    seePlayer = true;
                    SawPlayer?.Invoke();
                }
            }
            //else if (hit2d.collider != null && !hit2d.collider.gameObject.CompareTag("Player"))
            //{
            //    if (seePlayer)
            //    {
            //        seePlayer = false;
            //    }
            //}
            Debug.DrawRay(_collider.bounds.center, _direction * sightRange, lineColor);
            
        }
        
        return seePlayer;
    }

    void ChangePosition()
    {
        newPosition = new Vector3(Random.Range(-maxDistance, maxDistance),
            Random.Range(-maxDistance, maxDistance), 0);
    }
}

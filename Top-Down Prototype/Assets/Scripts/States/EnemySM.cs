using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySM : StateMachine
{
    #region Fields

    // Enemy States
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public EnemyIdle idleState;
    [HideInInspector]
    public AttackState attackState;

    // cached components
    private Animator _animator;
    private GameObject _player;
    private Collider2D _collider;
    private LayerMask detectionLayer;
    private bool facingRight = true;

    [Header("Sight Fields")]
    [SerializeField]
    private float sightRange;
    [SerializeField]
    private float attackRange;

    private GameObject[] wayPoints;

    #endregion

    #region Properties

    // Component Properties
    public GameObject Player => _player;
    public Collider2D Collider => _collider;
    public Animator Animator => _animator;

    // sight properties
    public float AttackRange => attackRange;

    // miscellaneous properties
    public LayerMask DetectionLayer => detectionLayer;
    public bool FacingRight => facingRight;

    public GameObject[] WayPoints => wayPoints;

    #endregion

    private void Awake()
    {
        chaseState = new ChaseState(this);
        idleState = new EnemyIdle(this);
        attackState = new AttackState(this);
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _collider = GetComponent<Collider2D>();
        detectionLayer = LayerMask.GetMask("Player", "Default");

        wayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }


    public bool CanSeePlayer()
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
                }
            }
            else if (hit2d.collider != null && !hit2d.collider.gameObject.CompareTag("Player"))
            {
                if (seePlayer)
                {
                    seePlayer = false;
                }
            }
        }
        Debug.DrawRay(_collider.bounds.center, _player.transform.position - transform.position, lineColor);
        return seePlayer;
    }

    public void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

    protected override BaseState GetInitialState() => idleState;
}

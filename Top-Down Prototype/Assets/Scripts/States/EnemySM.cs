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

    // cached components
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Rigidbody2D rb2d;
    private GameObject _player;

    private bool facingRight = true;

    [Header("Sight Fields")]
    [SerializeField]
    private float sightRange;
    [SerializeField]
    private float attackRange;

    #endregion

    #region Properties

    // Component Properties
    public GameObject Player => _player;
    public Collider2D Collider => _collider;
    public Animator Animator => _animator;
    public Rigidbody2D Rigidbody2D => rb2d;

    // sight properties
    public float AttackRange => attackRange;

    // miscellaneous properties
    public bool FacingRight => facingRight;


    #endregion

    private void Awake()
    {
        chaseState = new ChaseState(this);
        idleState = new EnemyIdle(this);
        _player = GameObject.FindGameObjectWithTag("Player");

    }

    public void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

    protected override BaseState GetInitialState() => idleState;

    private void OnEnable()
    {
        _collider.enabled = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Move))]
public class AIController : MonoBehaviour
{
    #region Fields

    [SerializeField] EnemyData data;
    IAttack attack;
    private Transform targetTransform;
    private Move enemyMove;
    private float distanceToTarget;
    private float nextAttack;
    private Animator _animator;
    private Collider2D _collider;
    private bool canMove = true;
    private Vector3 moveInput;

    #endregion

    private void Awake()
    {
        enemyMove = GetComponent<Move>();
        attack = GetComponent<IAttack>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag(data.TargetTag).transform;
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            distanceToTarget = Vector2.Distance(targetTransform.position, transform.position);
            if (distanceToTarget > data.MinAttackDistance &&
                distanceToTarget < data.MaxAttackDistance)
            {
                if (Time.time >= nextAttack && Time.timeScale > 0)
                {
                    attack.Attack();
                    StartCoroutine(PauseMovement());
                    nextAttack = Time.time + data.AttackCooldown;
                }
            }
        }
    }

    IEnumerator PauseMovement()
    {
        canMove = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().isKinematic = false;
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            moveInput = targetTransform.position - transform.position;
        }
        else
        {
            moveInput = Vector3.zero;
        }
        var speed = data.Speed;

        if (distanceToTarget > data.ChaseDistance)
        {
            enemyMove.MoveObject(moveInput.normalized, speed);
            _animator.SetFloat("Move Magnitude", moveInput.magnitude);
        }
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }
}

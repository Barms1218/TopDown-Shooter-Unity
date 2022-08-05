using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIController : MonoBehaviour
{
    //[SerializeField] float chaseDistance;


    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Collider2D _collider;
    [SerializeField] Animator _animator;
    [SerializeField] private float attackDistance;
    [SerializeField] private float minAttackDistance;
    private GameObject player;

    public Rigidbody2D Rigidbody2D => rb2d;
    public Collider2D Collider => _collider;
    public Animator Animator => _animator;

    public UnityAction<Vector2> moveDelegate;
    public UnityAction attackDelegate;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        var _distance = Vector2.Distance(player.transform.position, transform.position);
        if (_distance > minAttackDistance && _distance < attackDistance)
        {
            attackDelegate?.Invoke();
        }
    }


    void FixedUpdate()
    {
        Vector2 moveInput = player.transform.position;
        moveDelegate?.Invoke(moveInput);
    }
}

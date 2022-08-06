using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIController : Controller
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float minAttackDistance;
    private GameObject player;

    public UnityAction<Vector2> moveDelegate;
    public UnityAction attackDelegate;

    // Start is called before the first frame update
    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Update()
    {
        if (player != null)
        {
            var _distance = Vector2.Distance(player.transform.position, transform.position);
            if (_distance > minAttackDistance && _distance < attackDistance)
            {
                attackDelegate?.Invoke();
            }
        }
    }

    protected override void FixedUpdate()
    {
        Vector2 moveInput = player.transform.position;
        moveDelegate?.Invoke(moveInput);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeZombie : Enemy
{
    [SerializeField] private int attackStrength = 25;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float attackCooldown = 1;

    private float nextAttack;

    IHaveHealth targetHealth;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targetHealth = _player.GetComponent<IHaveHealth>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (_player != null)
        {
            var _distance = Vector2.Distance(_player.transform.position, transform.position);
            if (_distance <= attackRange)
                Attack();
        }
    }


    void Attack()
    {
        if (CanAttack() && targetHealth != null)
        {
            AudioManager.Play(AudioClipName.MeleeAttack);
            targetHealth.TakeDamage(damage, gameObject, attackStrength);
            nextAttack = Time.time + attackCooldown;
        }
    }

    private IEnumerator Stagger()
    {
        canMove = false;
        yield return new WaitForSeconds(attackCooldown);
        canMove = true;
    }

    private bool CanAttack() => Time.time >= nextAttack;
}

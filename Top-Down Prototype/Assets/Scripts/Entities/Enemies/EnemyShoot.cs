using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "AI Attacks/Ranged")]
public class EnemyShoot : AttackObject
{
    //[SerializeField] float maximumRange;
    //[SerializeField] float minimumRange;
    [SerializeField] Gun weapon;

    public override void Attack(AIController controller)
    {
        weapon.Fire(Vector2.right);
    }
}

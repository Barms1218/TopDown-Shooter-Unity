using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInmates : MonoBehaviour
{
    [SerializeField] EnemyData inmateData;
    [SerializeField] AttackObject inmateAttack;

    private void OnEnable()
    {
        inmateData.Speed *= 2;
        inmateAttack.Damage *= 2;
    }

    private void OnDisable()
    {
        inmateData.Speed /= 2;
        inmateAttack.Damage /= 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeZombie : Enemy
{


    protected override void Start()
    {
        base.Start();
        Points = 15;
    }

}

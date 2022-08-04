using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    float Speed { get; set; }
    float ChaseDistance { get; set; }
    bool FacingRight { get; set; }

    void Move();
}

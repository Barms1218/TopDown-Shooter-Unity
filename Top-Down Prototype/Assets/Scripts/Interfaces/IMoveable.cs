using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    float Speed { get; set; }
    bool FacingRight { get; }

    void MoveObject(Vector2 input);
}

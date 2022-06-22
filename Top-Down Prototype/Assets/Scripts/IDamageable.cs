using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveHealth
{
    float Health { get; set; }
    int MaxHealth{ get; }
    void TakeDamage(int amount);
    void RestoreHealth(int amount);
}


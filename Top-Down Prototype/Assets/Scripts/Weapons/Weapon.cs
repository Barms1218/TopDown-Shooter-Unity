using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Fields

    Rigidbody2D body;

    //public int maxAmmunition;
    //public float reloadSpeed;

    #endregion

    #region Properties


    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public abstract void Shoot();

    public abstract void Reload();
}

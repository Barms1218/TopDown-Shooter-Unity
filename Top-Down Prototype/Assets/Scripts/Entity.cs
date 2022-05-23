using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected InputController input = null;
    protected Rigidbody2D body;

    // Events
    public delegate void ShootInput();
    public static event ShootInput OnShoot;
    public delegate void ReloadInput();
    public static event ReloadInput OnReload;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (input.RetrieveMouseButtonZero())
        {
            OnShoot?.Invoke();
        }

        if (input.RetrieveReloadInput())
        {
            OnReload?.Invoke();
        }
    }
}

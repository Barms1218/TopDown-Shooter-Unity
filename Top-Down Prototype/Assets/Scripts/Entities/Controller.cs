using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected Collider2D _collider;
    [SerializeField]
    protected Animator _animator;

    public Rigidbody2D Rigidbody2D => rb2d;
    public Collider2D Collider => _collider;
    public Animator Animator => _animator;

    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }
    protected virtual void Start() { }

    // Update is called once per frame
    protected virtual void Update() { }

    protected abstract void FixedUpdate();
}

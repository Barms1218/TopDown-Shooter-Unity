using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public bool Interact { get; private set; }
    public bool Reload { get; private set; }
    public bool Fire { get; private set; }
    public bool SpecialAttack { get; private set; }
    public bool[] SwapWeapon { get; private set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public static UnityAction OnReload;
    public static UnityAction OnFire;
    public static UnityAction OnSpecialAttack;

    OrientPlayer player;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        player = GetComponent<OrientPlayer>();
    }
    // Update is called once per frame
    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        Reload = Input.GetKeyDown(KeyCode.R);
        SpecialAttack = Input.GetKeyDown(KeyCode.F);
        Fire = Input.GetMouseButton(0);
        if (Reload)
        {
            OnReload?.Invoke();
        }
        if (Fire)
        {
            OnFire?.Invoke();
        }
        if (SpecialAttack)
        {
            OnSpecialAttack?.Invoke();
        }
    }
}

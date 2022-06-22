using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public bool Interact { get; private set; }
    public bool Reload { get; private set; }
    public bool Fire { get; private set; }
    public bool[] SwapWeapon { get; private set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public static UnityAction OnReload;
    public static UnityAction OnFire;

    IEnumerator coroutine;
    Player player;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        player = GetComponent<Player>();
    }
    // Update is called once per frame
    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        Reload = Input.GetKeyDown(KeyCode.R);
        Fire = Input.GetMouseButtonDown(0);
        coroutine = ContinuousFire();
        if (Reload)
        {
            OnReload?.Invoke();
        }
        if (Fire)
        {
            StartCoroutine(coroutine);
        }
        else if (!Fire)
        {
            StopCoroutine(coroutine);
        }
    }

    IEnumerator ContinuousFire()
    {
        while (true)
        {
            OnFire?.Invoke();
            yield return new WaitForSeconds(player.CurrentWeapon.TimeBetweenShots);
        }
    }
}

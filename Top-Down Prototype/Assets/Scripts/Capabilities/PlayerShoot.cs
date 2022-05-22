using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] InputController input = null;
    public static UnityAction shootInput;
    public static UnityAction reloadInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input.RetrieveMouseButtonZero())
        {
            shootInput?.Invoke();
        }

        if (input.RetrieveReloadInput())
        {
            reloadInput?.Invoke();
        }
    }
}

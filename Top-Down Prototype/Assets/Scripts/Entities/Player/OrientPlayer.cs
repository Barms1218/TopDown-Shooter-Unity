using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrientPlayer : MonoBehaviour
{
    #region Fields
    
    private bool facingRight = true;

    #endregion

    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
    }

}

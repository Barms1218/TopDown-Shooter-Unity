using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCountText;

    // Start is called before the first frame update
    void Start()
    {
        ammoCountText.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolPanel : MonoBehaviour
{
    Image image;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
    }

    public void ActivateWeaponColor(string activeString)
    {
        if (activeString == "active")
        {
            color = new Color(0f, 0f, 0f, 45f);
        }
        else if (activeString == "inactive")
        {
            color = new Color(255f, 255, 255f, 45f);
        }
    }
}

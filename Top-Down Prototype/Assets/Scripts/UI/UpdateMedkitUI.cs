using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMedkitUI : MonoBehaviour
{
    public IntVariable medkitCount;
    public TextMeshProUGUI medkitText;
    private int countValue;

    private void Update()
    {
        countValue = medkitCount.Value;
        medkitText.text = "x" + countValue.ToString();
    }
}

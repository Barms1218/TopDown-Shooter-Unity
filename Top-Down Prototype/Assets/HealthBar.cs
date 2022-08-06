using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Health health;
    [SerializeField] private Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    [SerializeField] Image healthBarImage;

    public float MaxValue
    {
        get { return slider.maxValue; }
        set { slider.maxValue = value; }
    }

    public void ChangeValue(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (slider.normalizedValue < 1)
        {
            healthBarImage.enabled = true;
            fill.enabled = true;
        }
    }

    private void Awake()
    {
        health = GetComponentInParent<Health>();
    }
    private void OnEnable()
    {
        health.onHit += ChangeValue;
        fill.color = gradient.Evaluate(1f);
        fill.enabled = false;
        healthBarImage.enabled = false;
    }
}

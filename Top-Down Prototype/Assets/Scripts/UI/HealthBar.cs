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
    private bool hasBeenHit = false;

    private void Awake()
    {
        health = GetComponentInParent<Health>();
    }

    private void Update()
    {
        slider.value = health.CurrentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (slider.normalizedValue < 1 && !hasBeenHit)
        {
            hasBeenHit = true;
            healthBarImage.enabled = true;
            fill.enabled = true;
        }
    }
    private void OnEnable()
    {
        slider.maxValue = health.MaxHealth;
        fill.color = gradient.Evaluate(1f);
        fill.enabled = false;
        healthBarImage.enabled = false;
        hasBeenHit = false;
    }
}

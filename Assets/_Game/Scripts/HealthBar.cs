using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    public void SetHealth(int health, int maxHealth)
    {
        Slider.gameObject.SetActive(health != 0 && health < maxHealth);
        Slider.minValue = 0;
        Slider.maxValue = maxHealth;
        Slider.value = health;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);

    }
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{    
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI valueText;

    public void UpdateHealth(int currentValue, int maxValue){
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
        valueText.text = $"{currentValue}/{maxValue}";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image fillBar;

    public TextMeshProUGUI valueText;


    public void UpdateHealth(int currentValue, int maxValue){
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
        valueText.text = currentValue.ToString() + "/" + maxValue.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
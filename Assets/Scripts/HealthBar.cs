using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public void UpdateHealthBar(float curHealth, float maxHealth)
    {
        if (healthBar)
        {
            healthBar.fillAmount = curHealth / maxHealth;
        }
    }
}

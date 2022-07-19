using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _healthBarFilled;
    public void SetMaxHealth(int maxHealth)
    {
        _healthBarFilled.fillAmount = 1f;
    }

    public void SetCurrentHealth(int maxHealth, int health)
    {
        _healthBarFilled.fillAmount = (float) health / maxHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
   [SerializeField] private Slider healthBar;

    public void GiveFullHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void setHealth(float health)
    {
    
        healthBar.value = health;
    }
}

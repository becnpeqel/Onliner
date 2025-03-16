using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerHealthBar : MonoBehaviour
{
    private Slider _healthBar;
    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    //Это Ванюша если что.
    private void Update()
    {
        
    }
    public void SetMaxHealth(int maxHealth)
    {
        _healthBar.maxValue = maxHealth;    

    }

    public void OnHealthChanged(int health)
    {
         _healthBar.value = health;
    }
}
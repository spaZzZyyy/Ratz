using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    Slider slider;
    [SerializeField] healthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = healthManager.maxHealth;
        slider.minValue = 0;
        slider.value = healthManager.maxHealth;
    }

    private void OnEnable() {
        Actions.PlayerTookDamage += takeDamage;
        Actions.PlayerHealed += takeHeal;
    }
    
    private void OnDisable() {
        Actions.PlayerTookDamage -= takeDamage;
        Actions.PlayerHealed -= takeHeal;
    }

    void takeDamage(){
        slider.value--;
    }

    void takeHeal(){
        slider.value++;
    }
}

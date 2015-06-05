using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public Slider HealthBar;
    public Image Fill;

    // Use this for initialization
    void Start()
    {
        Fill.color = Color.green;
    }
    void Damage()
    {
        HealthBar.value -= 0.05F;
    }
    void Heal()
    {
        HealthBar.value += 0.05F;
    }
    void Update()
    {
        if (HealthBar.value <= 0.5F)
        {
            Fill.color = Color.yellow;
        }
        if (HealthBar.value > 0.5F)
        {
            Fill.color = Color.green;
        }
        if (HealthBar.value < 0.2F)
        {
            Fill.color = Color.red;
        }
        if (HealthBar.value > 0.2F&&HealthBar.value<=0.5F)
        {
            Fill.color = Color.yellow;
        }
        if (HealthBar.value == 0)
        {
            Fill.color = Color.black;
        }
        
    }
}

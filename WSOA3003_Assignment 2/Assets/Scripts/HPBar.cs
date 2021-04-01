using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Chara_Info target;
    public Slider healthbar;


 



    void Start()
    {
        
        
            healthbar.maxValue = target.maxHP;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        healthbar.value = target.currentHP;
        


        
    }
}

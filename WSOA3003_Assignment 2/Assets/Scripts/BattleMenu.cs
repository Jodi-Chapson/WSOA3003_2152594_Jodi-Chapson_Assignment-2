using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenu : MonoBehaviour
{
    public GameObject baseMenu, specialMenu;
    public StateMachine statemachine;
    public GameObject specialbutton;
    public GameObject battlehud;

    public Text playerhp, playerdmg, enemyhp, enemydmg;

    public Image headsprite;
    public GameObject Arrow;
   
    



    



    public void Togglespecialskill()
    {
        specialbutton.SetActive(true);
    }

    public void ToggleBaseorBack()
    {
        baseMenu.SetActive(true);
        specialMenu.SetActive(false);
    }

    public void ToggleSpecial()
    {
        baseMenu.SetActive(false);
        specialMenu.SetActive(true);
    }

    public void ToggleOFFALLMenus()
    {
        baseMenu.SetActive(false);
        specialMenu.SetActive(false);
    }

    public void ToggleONBATTLEHUD()
    {
        battlehud.SetActive(true);

    }

    public void ToggleOFFBATTLEHUD()
    {
        battlehud.SetActive(false);

    }

    public void Update()
    {
        if (statemachine.playerinfo.currentHP <= 0)
        {
            statemachine.playerinfo.currentHP = 0;
        }
        playerhp.text = statemachine.playerinfo.currentHP.ToString();
        playerdmg.text = statemachine.playerinfo.damage.ToString();

        if (battlehud.activeSelf == true)
        {
            

            if (statemachine.playerinfo.currentHP <= 0)
            {
                statemachine.playerinfo.currentHP = 0;
            }
            if (statemachine.enemyinfo.currentHP <= 0)
            {
                statemachine.enemyinfo.currentHP = 0;
            }
            playerhp.text = statemachine.playerinfo.currentHP.ToString();
            playerdmg.text = statemachine.playerinfo.damage.ToString();

            enemyhp.text = statemachine.enemyinfo.currentHP.ToString();
            enemydmg.text = statemachine.enemyinfo.damage.ToString();

            

        }
    }












}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara_Info : MonoBehaviour
{
    public string UnitName;
    public int type; //types: 0 = protag, 1 = base enemy, 2 = elemental enemy
    public int element;  // 0 = none, 1 = fire, 2 = water
    public int damage;
    public int maxHP;
    public int currentHP;
    public StateMachine gamestate;
    public Animator targetanimator;




    //public void Start()
    //{
    //    damage = damage * level;
    //    maxHP = maxHP * level;
    //}

    public void TakeDamage()
    {

    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {


            gamestate.state = BattleState.START;
            StartCoroutine(gamestate.BattleSetup());
            gamestate.enemyinfo = this.gameObject.GetComponent<Chara_Info>();
            gamestate.enemy = this.gameObject;


        }
    }


}

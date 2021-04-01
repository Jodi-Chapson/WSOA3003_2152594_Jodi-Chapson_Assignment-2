using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //script for casual character movement
    
    [Header("References")]
    public Rigidbody2D rb;
    public float Speed;
    public float percentMod;
    public GameObject sprite;
    public bool canmove;
    public Animator playeranim;

    public StateMachine statemachine;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        canmove = true;
    }


    public void Update()
    {
        if(statemachine.state == BattleState.OUTCOMBAT)
        {
            if(rb.velocity == Vector2.zero)
            {
                    
                playeranim.SetInteger("State",0);
            }
            else
            {
                
                playeranim.SetInteger("State", 1);
            }

        }
        
    }




    public void FixedUpdate()
    {

        if (canmove)
        {
            if (Input.GetKey("d"))
            {
                rb.velocity = new Vector2(Speed, rb.velocity.y);
                Flip();
            }
            else if (Input.GetKey("a"))
            {
                rb.velocity = new Vector2(-Speed, rb.velocity.y);

                Flip();
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }


            if (Input.GetKey("w"))
            {
                rb.velocity = new Vector2(rb.velocity.x, Speed * percentMod);
            }
            else if (Input.GetKey("s"))
            {
                rb.velocity = new Vector2(rb.velocity.x, -Speed * percentMod);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

    }

    public void Flip()
    {
        // flipping the character sprite


        if (rb.velocity.x > 0)
        {
            
            sprite.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (rb.velocity.x < 0)
        {
            sprite.transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }
}

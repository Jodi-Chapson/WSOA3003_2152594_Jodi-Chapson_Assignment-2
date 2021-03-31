using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Reference for the state machine/combat system from Brackey's "Turn-Based Combat in Unity" https://www.youtube.com/watch?v=_1pz_ohupPs&t
public enum BattleState { OUTCOMBAT, START, PLAYERTURN, ENEMYTURN, END }

public class StateMachine : MonoBehaviour
{
	public GameObject player, enemy;
	public Chara_Info playerinfo, enemyinfo;
	public Transform playerPos, enemypos, camplacement;
	public Vector3 playerlastPos, enemylastPos, camlastpos;
	public BattleState state;
	public CharacterMovement playermove;
	public CamController cam;
	public Animator fadeanim;
	public BattleMenu BM;
	public GameObject key, support;


	public GameObject door;
	public Sprite openeddoor;
	public GameObject endscreen;
	public Support supp;
	


	public void Start()
	{
		state = BattleState.OUTCOMBAT;
		
	}



	

	public IEnumerator BattleSetup()
	{
		yield return new WaitForSeconds(0.05f);

		fadeanim.Play("Toblack", 0, 0.0f);

		BM.ToggleONBATTLEHUD();

		cam.CamFollow = false;
		playerlastPos = player.transform.position;
		enemylastPos = enemy.transform.position;
		camlastpos = cam.transform.position;
		player.transform.position = playerPos.transform.position;
		enemy.transform.position = enemypos.transform.position;

		cam.transform.position = camplacement.transform.position;
		playermove.sprite.transform.eulerAngles = new Vector3(0, 0, 0);
		
		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		playermove.canmove = false;

		yield return new WaitForSeconds(0.1f);

		fadeanim.Play("Fadeout", 0, 0.0f);

		yield return new WaitForSeconds(0.5f);

		state = BattleState.PLAYERTURN;

		PlayerTurn();
	}

	public void PlayerTurn()
	{
		BM.ToggleBaseorBack();


	}

	public void ToggleFlee()
	{
		StartCoroutine(Flee());
	}
	public IEnumerator Flee ()
	{
		yield return new WaitForSeconds (0.3f);

		fadeanim.Play("Toblack", 0, 0.0f);

		BM.ToggleOFFALLMenus();

		//player.transform.position = playerlastPos;
		enemy.transform.position = enemylastPos;
		playermove.canmove = true;
		cam.CamFollow = true;
		cam.transform.position = camlastpos;

		Vector3 newpos = new Vector3((-enemylastPos.x + 2*playerlastPos.x), (-enemylastPos.y + 2*playerlastPos.y), 0);
		playerinfo.currentHP -= 1;
		
		//a basic highschool maths formula that took me way too long to get haha
		player.transform.position = newpos;
		
		yield return new WaitForSeconds(0.1f);

		fadeanim.Play("Fadeout", 0, 0.0f);




		yield return new WaitForSeconds(0.5f);



	}
	
	public void toggleattack(int type)
	{
		if (type == 1)
		{
			StartCoroutine(PlayerAttack()); 
		}
		else if (type == 2)
		{
			StartCoroutine(WaterAttack());
		}

			

		
	}

	public IEnumerator PlayerAttack()
	{
		yield return new WaitForSeconds(1f);

		BM.ToggleOFFALLMenus();
		enemyinfo.currentHP -= playerinfo.damage;

		yield return new WaitForSeconds(1f);


		if (enemyinfo.currentHP <= 0)
		{
			state = BattleState.END;
			StartCoroutine(EndFight(1));

		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyAttack());
		}

	}

	public IEnumerator WaterAttack()
	{
		yield return new WaitForSeconds(1f);

		BM.ToggleOFFALLMenus();
		if (enemyinfo.type == 1)
		{
			enemyinfo.currentHP -= playerinfo.damage*2;
		}
		else
		{
			enemyinfo.currentHP -= playerinfo.damage + (int) playerinfo.damage/2;
		}


		yield return new WaitForSeconds(1f);


		if (enemyinfo.currentHP <= 0)
		{
			state = BattleState.END;
			StartCoroutine(EndFight(1));

		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyAttack());
			
			
		}
	}


	public IEnumerator EnemyAttack()
	{
		yield return new WaitForSeconds(1f);

		playerinfo.currentHP -= enemyinfo.damage;


		yield return new WaitForSeconds(1f);


		if (playerinfo.currentHP <= 0)
		{
			state = BattleState.END;
			StartCoroutine(EndFight(2));
		}
		else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}


	}



	public IEnumerator EndFight(int conclusion)
	{
		yield return new WaitForSeconds(0.3f);

		fadeanim.Play("Toblack", 0, 0.0f);

		BM.ToggleOFFALLMenus();
		BM.ToggleOFFBATTLEHUD();

		if (conclusion == 1)
		{
			if (enemyinfo.type == 1)
			{
				//drop elemental
				supp.canfollow = true;
				BM.Togglespecialskill();

			}
			else if (enemyinfo.type == 2)
			{
				UnlockDoor();

				yield return new WaitForSeconds(0.5f);
				endscreen.SetActive(true);

			}


			player.transform.position = playerlastPos;
			Destroy(enemy);
			playermove.canmove = true;
			cam.CamFollow = true;
			cam.transform.position = camlastpos;

		}
		else if (conclusion == 2)
		{

			endscreen.SetActive(true);
		}




		yield return new WaitForSeconds(0.1f);

		fadeanim.Play("Fadeout", 0, 0.0f);

		yield return new WaitForSeconds(0.5f);


		state = BattleState.OUTCOMBAT;


		
		


	}

	public void UnlockDoor()
	{
		door.GetComponent<SpriteRenderer>().sprite = openeddoor;

		
	}

	public void End()
	{
		endscreen.SetActive(true);
	}
	


}

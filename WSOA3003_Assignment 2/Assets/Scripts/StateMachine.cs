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


	public Slider enemyhealth;
	


	public void Start()
	{
		state = BattleState.OUTCOMBAT;
		
	}



	

	public IEnumerator BattleSetup()
	{
		yield return new WaitForSeconds(0.05f);

		fadeanim.Play("Toblack", 0, 0.0f);

		playermove.playeranim.SetInteger("State", 0);

		BM.ToggleONBATTLEHUD();

		cam.CamFollow = false;
		playerlastPos = player.transform.position;
		enemylastPos = enemy.transform.position;
		camlastpos = cam.transform.position;
		player.transform.position = playerPos.transform.position;
		enemy.transform.position = enemypos.transform.position;

		enemyhealth.maxValue = enemy.GetComponent<Chara_Info>().maxHP;
		enemyhealth.value = enemy.GetComponent<Chara_Info>().currentHP;

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
		BM.Arrow.SetActive(true);
		BM.Arrow.transform.localPosition = new Vector2(-420, BM.Arrow.transform.localPosition.y);


	}

	public void ToggleFlee()
	{
		StartCoroutine(Flee());
	}
	public IEnumerator Flee ()
	{
		yield return new WaitForSeconds (0.2f);

		BM.Arrow.SetActive(false);
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


		BM.Arrow.SetActive(false);

	}

	public IEnumerator PlayerAttack()
	{
		BM.ToggleOFFALLMenus();
		
		
		yield return new WaitForSeconds(0.5f);

		playermove.playeranim.Play("attack", 0, 0.0f);

		yield return new WaitForSeconds(0.6f);


		enemyinfo.currentHP -= playerinfo.damage;
		enemyhealth.value = enemy.GetComponent<Chara_Info>().currentHP;

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
		BM.ToggleOFFALLMenus();


		yield return new WaitForSeconds(0.5f);
		playermove.playeranim.Play("attack", 0, 0.0f);

		yield return new WaitForSeconds(0.6f);


		if (enemyinfo.type == 1)
		{
			enemyinfo.currentHP -= playerinfo.damage*2;
			enemyhealth.value = enemy.GetComponent<Chara_Info>().currentHP;
		}
		else
		{
			enemyinfo.currentHP -= playerinfo.damage + (int) playerinfo.damage/2;
			enemyhealth.value = enemy.GetComponent<Chara_Info>().currentHP;
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
		BM.Arrow.transform.localPosition = new Vector2(420, BM.Arrow.transform.localPosition.y);
		BM.Arrow.SetActive(true);

		yield return new WaitForSeconds(2f);

		playerinfo.currentHP -= enemyinfo.damage;
		playermove.playeranim.Play("flinch", 0, 0.0f);


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
		BM.Arrow.SetActive(false);

		yield return new WaitForSeconds(0.3f);

		fadeanim.Play("Toblack", 0, 0.0f);

		BM.ToggleOFFALLMenus();
		BM.ToggleOFFBATTLEHUD();

		playermove.canmove = true;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	//firePoint where enemies's weapon get instantiate
	public Transform firePoint;
	//enemies weapon
	public GameObject weapon;
	//enemies attack animator
	public Animator attack;

	//enemies state
	public enum EnemyState{ALIVE,DIED};
	public EnemyState enemyState;

	GameObject Weapon;

	//used if enemy has weapon or not
	bool haveWeapon = false;

	void Start()
	{
		//invokes shoot after 1sec 
		Invoke ("Shoot",1f);
		//sets enemy state to alive
		enemyState = EnemyState.ALIVE;
	}

	void Shoot()
	{
		//this block runs only if game state is play and enemy is alive
		if(Manager.State == Manager.gameState.PLAY && enemyState == EnemyState.ALIVE)
		{
			//perform the enemy's attack animation
			attack.SetBool ("Attack",true);

			//instantiate enemy's weapon
			Weapon = Instantiate (weapon,firePoint.position,firePoint.rotation);
			//sets the firepoint as parent so that it follows the animation
			Weapon.transform.parent = firePoint.transform;

			//sets true as weapon is instantiated 
			haveWeapon = true;

			//invokes weapon throwing method fire after 0.45secs
			Invoke("Fire",0.45f);
		}
		//if game is paused this block runs and check the game state
		if (Manager.State == Manager.gameState.PAUSE || Manager.State == Manager.gameState.LEVELUPGRADE || Manager.State == Manager.gameState.GAMEOVER)
			Invoke ("CheckResume",1f);
	}

	//throws the weapon
	void Fire()
	{
		//this block runs only if enemy is alive and game state is play
		if (Manager.State == Manager.gameState.PLAY && enemyState == EnemyState.ALIVE) 
		{
			//sets parent null as no longer need to follow firepoint
			Weapon.transform.SetParent (null);
			//sets body type to dynamic so force can be applied
			Weapon.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
			Weapon.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (Random.Range (-60f, -30f), Random.Range (1f, 5f)), ForceMode2D.Force); 
			Weapon.GetComponent<Rigidbody2D> ().AddTorque (5f);
			//set enemy back to normal position (idle position)
			attack.SetBool ("Attack", false);

			//sets false as weapon has been throwen
			haveWeapon = false;

			//invoke shoot after 2sesc so that enemy attack again 
			if (Manager.State == Manager.gameState.PLAY)
				Invoke ("Shoot", 2f);
		}

		//if game is paused this block runs and check the game state
		if (Manager.State == Manager.gameState.PAUSE  || Manager.State == Manager.gameState.LEVELUPGRADE || Manager.State == Manager.gameState.GAMEOVER)
			Invoke ("CheckResume",1f);
	}
		
	void CheckResume()
	{
		//if game state is still pause it checks game state again after 1sec
		if (Manager.State == Manager.gameState.PAUSE  || Manager.State == Manager.gameState.LEVELUPGRADE || Manager.State == Manager.gameState.GAMEOVER)
			Invoke ("CheckResume", 1f);
		else 
		{
			//if enemy has weapon it throws the weapon after resume 
			if (haveWeapon)
				Invoke ("Fire",0f);
			//if enemy has not weapon it instantiate the weapon after resume
			else
				Invoke ("Shoot", 2f);
		}
	}

}

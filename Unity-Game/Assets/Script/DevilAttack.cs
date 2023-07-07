using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilAttack : MonoBehaviour {

	//firePoint where enemies's weapon get instantiate
	public Transform firePoint;
	//enemies weapon
	public GameObject weapon;
	//enemies attack animator
	public Animator attack;

	//enemies state
	public enum DevilState{ALIVE,DIED};
	public DevilState devilState;

	GameObject Weapon;

	//used if devil has weapon or not
	bool haveWeapon = false;

	void Start()
	{
		//invokes shoot after 1sec 
		Invoke ("Shoot",1f);
		//sets devil state to alive
		devilState = DevilState.ALIVE;
	}

	void Shoot()
	{
		//this block runs only if game state is play and devil is alive
		if(Manager.State == Manager.gameState.PLAY && devilState == DevilState.ALIVE)
		{
			//perform the devil's attack animation
			attack.SetBool ("Fire",true);
			//instantiate devil's weapon
			Weapon = Instantiate (weapon,firePoint.position,firePoint.rotation);
			//sets the firepoint as parent so that it follows the animation
			Weapon.transform.parent = firePoint.transform;

			//sets true as weapon is instantiated 
			haveWeapon = true;

			//invokes weapon throwing method fire after 0.45secs
			Invoke("Fire",1.2f);
		}
		//if game is paused this block runs and check the game state
		if (Manager.State == Manager.gameState.PAUSE || Manager.State == Manager.gameState.LEVELUPGRADE || Manager.State == Manager.gameState.GAMEOVER)
			Invoke ("CheckResume",1f);
	}

	//throws the weapon
	void Fire()
	{
		//this block runs only if devil is alive and game state is play
		if (Manager.State == Manager.gameState.PLAY && devilState == DevilState.ALIVE) 
		{
			//sets parent null as no longer need to follow firepoint
			Weapon.transform.SetParent (null);
			//sets body type to dynamic so force can be applied
			Weapon.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
			Weapon.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (Random.Range (-70f, -50f), Random.Range (3f, 7f))*3); 
			Weapon.GetComponent<Rigidbody2D> ().AddTorque (15f);

			//set devil back to normal position (idle position)
			attack.SetBool ("Fire", false);

			//sets false as weapon has been throwen
			haveWeapon = false;

			//invoke shoot after 2sesc so that devil attack again 
			if (Manager.State == Manager.gameState.PLAY)
				Invoke ("Shoot", 2f);
		}

		//if game is paused this block runs and check the game state
		if (Manager.State == Manager.gameState.PAUSE || Manager.State == Manager.gameState.LEVELUPGRADE || Manager.State == Manager.gameState.GAMEOVER)
			Invoke ("CheckResume",1f);
	}

	void CheckResume()
	{
		//if game state is still pause it checks game state again after 1sec
		if (Manager.State == Manager.gameState.PAUSE || Manager.State == Manager.gameState.LEVELUPGRADE || Manager.State == Manager.gameState.GAMEOVER)
			Invoke ("CheckResume", 1f);
		else 
		{
			//if devil has weapon it throws the weapon after resume 
			if (haveWeapon)
				Invoke ("Fire",0f);
			//if devil has not weapon it instantiate the weapon after resume
			else
				Invoke ("Shoot", 2f);
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	//Attack script static Instance
	public static Attack attackObj;

	//firePoint where ninja's weapon gets instantiate
	public Transform firePoint;
	//weapon's array 
	public GameObject[] weapon;
	//attack animator
	public Animator attack;

	//boolean for attack dealy
	public bool fire;
	public float minSpeed = 20f, maxSpeed = 50f, respawnRate;

	//which weapon to instantiate
	GameObject Weapon;

	//attack speed
	public float speed;

	void Awake()
	{
		//initalize aatack script static instance 
		attackObj = this.gameObject.GetComponent<Attack> ();
	}

	void Start()
	{
		speed = minSpeed;
		setFireRate ();
		//get weapon
		Invoke("instantiateWeapon",0.5f);
	}

	void Update () 
	{

		//this block runs only if game state is play and ninja is alive
		if((Manager.State == Manager.gameState.PLAY || Manager.State == Manager.gameState.FIRSTTIME) && NinjaHealth.ninjaState == NinjaHealth.NinjaState.ALIVE) 
		{
			//increase the speed of throwing weapon
			#if !UNITY_ANDROID
			if (Input.GetKey (KeyCode.Mouse0) && fire) 
			{
				speed += 2f;
				//sets the max speed of throwing weapon
				speed = Mathf.Clamp (speed, minSpeed, maxSpeed);
			} 
			//when wouse left key is released shoot method runs
			if (Input.GetKeyUp (KeyCode.Mouse0) && fire)
				Shoot ();
			#endif
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && fire)
			{
				speed += 2f;
				//sets the max speed of throwing weapon
				speed = Mathf.Clamp (speed, minSpeed, maxSpeed);
			}
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && fire)
				Shoot ();
		}

		//when game goes to pause mode this block sets false for fire
		if (Manager.State == Manager.gameState.PAUSE || Manager.State == Manager.gameState.LEVELUPGRADE)
			fire = false;
	}
	//respawn dealay of weapons
	void setFireRate ()
	{
		if (DatabaseManager.Instance.GetLocalData() != null)
		{
			LocalData data = DatabaseManager.Instance.GetLocalData();

			int index = data.Weapon;
			if (index < 5)
				respawnRate = 0.6f;
			else if (index == 5)
				respawnRate = 0.7f;
			else if (index == 6 || index == 7)
				respawnRate = 0.5f;
			else if (index == 8)
				respawnRate = 0.4f;
			else if (index == 9 || index == 10)
				respawnRate = 0.3f;
			else if (index == 11)
				respawnRate = 0.2f;
			else if (index == 12)
				respawnRate = 0.4f;
		}
	}
	//Starts the animation of attack
	void Shoot ()
	{
		fire = false;

		//attack animation
		attack.SetBool ("Attack",true);

		//invokes the weapon throwing methos
		Invoke ("Fire", 0.2f);
	}

	void Fire()
	{
		LocalData data = DatabaseManager.Instance.GetLocalData();
		if (data == null) return;

		//removes the parent because no longer needed to follow firePoint
		Weapon.transform.SetParent (null);
		//sets body type to dynamic so force can be applied 
		Weapon.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;

		//Rotate Weapon towards the mouse direction
		Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 direction = new Vector2 (mouse.x - Weapon.transform.position.x,mouse.y - Weapon.transform.position.y);
		Weapon.transform.right = direction;

		Weapon.GetComponent<Rigidbody2D> ().AddForce(direction*speed);
		audioManager.instance.PlaySound ("ThrowingWeapon");

		//if Weapon is star
		if(data.Weapon<6 || data.Weapon == 12)
			Weapon.GetComponent<Rigidbody2D> ().AddTorque (-5f);

		//get weapon to attack again
		Invoke ("instantiateWeapon",respawnRate);

		//set back to normal position of ninja (Idle position)
		attack.SetBool ("Attack",false);
		//set weapon throwing speed back to default
		speed = minSpeed;
	}

	void instantiateWeapon()
	{
		LocalData data = DatabaseManager.Instance.GetLocalData();
		if (data == null) return;
		//gets the which weapon to instastiate
		int weaponIndex = data.Weapon;

		//instantiate the weapon
		Weapon = Instantiate (weapon[weaponIndex],firePoint.position,firePoint.rotation);

		//sets the firepoint as parent so that it follows the attack animation
		Weapon.transform.parent = firePoint.transform;

		//set true so that ninja can attack again
		fire = true;
	}
}

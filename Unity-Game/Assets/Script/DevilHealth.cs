using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilHealth : MonoBehaviour {

	//enemy's hitpoint
	public float hitPoint = 10f;

	void Start()
	{
		LocalData data= DatabaseManager.Instance.GetLocalData();
		if (data == null) return;

		hitPoint = 10 + (int)(data.Level / 3);
		
	}

	public void fall()
	{
		LocalData data = DatabaseManager.Instance.GetLocalData();
		if (data == null) return;

		//increase the kill
		Score.score += 1;

		int money, i;
		//increase the money

		money = data.coins = data.coins+=15;
		Score.moneyIncrement = 15;
		GameObject.Find ("ScoreUI").GetComponent<Score> ().updateTargetKill ();

		//store money in player pref
		

		GameObject.Find ("ScoreUI").GetComponent<Score> ().UpdateScore ();

		audioManager.instance.PlaySound ("Die");

		//plays the blood ParticleSystem
		this.gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
		//disable the animator
		this.gameObject.GetComponent<Animator> ().enabled = false;
		//sets the enemy state ti died
		this.gameObject.GetComponent<DevilAttack> ().devilState = DevilAttack.DevilState.DIED;
		//disable the attack script of died enemy
		this.gameObject.GetComponent<DevilAttack> ().enabled = false;
		//gets all body parts rigidbody
		Rigidbody2D[] Devil = this.gameObject.GetComponentsInChildren<Rigidbody2D>();

		//sets all body parts body type to dynamic and gives the force for falling effect
		for (i = 0; i < Devil.Length; i++) 
		{
			Devil [i].bodyType = RigidbodyType2D.Dynamic;

			//untagging enemy's body parts so that when weapon collide with enemy after death it doesn't run the script
			//and avoid the run time error of already distroyed the enemy's plateform
			Devil [i].tag = "Untagged";
		}
		Devil [i-1].AddForce (new Vector2( Random.Range(500f,1000f), Random.Range(500f,1000f)));
		//at last disable this script as well
		this.gameObject.GetComponent<DevilHealth> ().enabled = false;

		DatabaseManager.Instance.UpdateData(data);
	}

}

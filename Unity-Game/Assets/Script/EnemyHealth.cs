using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public GameObject HeadShotImg;


	//enemy's hitpoint
	public float hitPoint = 2f;
	public bool headShot = false;

	public void fall()
	{

		LocalData data = DatabaseManager.Instance.GetLocalData();
		if (data == null) return;
		//increase the kill
		Score.score += 1;

		int money, i;
		//increase the money
		if (headShot)
		{

			money =data.coins = data.coins+ 10;
			Score.moneyIncrement = 10;
			GameObject.Find ("ScoreUI").GetComponent<Score> ().updateTargetHeadShot ();
			GameObject.Find ("ScoreUI").GetComponent<Score> ().updateTargetKill ();
			data.AchievementHeadshot += 1;
			data.AchievementKill += 1;

			
		}
		else 
		{
			money = data.coins = data.coins + 5;
			Score.moneyIncrement = 5;
			GameObject.Find ("ScoreUI").GetComponent<Score> ().updateTargetKill ();
			data.AchievementKill += 1;
			
		}

		DatabaseManager.Instance.UpdateData(data);
		

		GameObject.Find ("ScoreUI").GetComponent<Score> ().UpdateScore ();

		audioManager.instance.PlaySound ("Die");

		//plays the blood ParticleSystem
		this.gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
		//disable the animator
		this.gameObject.GetComponent<Animator> ().enabled = false;
		//sets the enemy state ti died
		this.gameObject.GetComponent<EnemyAttack> ().enemyState = EnemyAttack.EnemyState.DIED;
		//disable the attack script of died enemy
		this.gameObject.GetComponent<EnemyAttack> ().enabled = false;
		//gets all body parts rigidbody
		Rigidbody2D[] Enemy = this.gameObject.GetComponentsInChildren<Rigidbody2D> ();

		//sets all body parts body type to dynamic and gives the force for falling effect
		for (i = 0; i < Enemy.Length; i++) 
		{
			Enemy [i].bodyType = RigidbodyType2D.Dynamic;

			//untagging enemy's body parts so that when weapon collide with enemy after death it doesn't run the script
			//and avoid the run time error of already distroyed the enemy's plateform
			Enemy [i].tag = "Untagged";
			if (headShot) 
			{
				if (Enemy [i].gameObject.name == "EnemyHead") 
				{
					GameObject Img = Instantiate (HeadShotImg, Enemy [i].transform.position + new Vector3(0f,0.5f,0f), Enemy [i].transform.rotation);
					iTween.ScaleFrom (Img, iTween.Hash ("scale",new Vector3(0f,0f,0f),"time",1f));
					Destroy (Img,1.5f);
				}
			}
		}
		Enemy [i-1].AddForce (new Vector2( Random.Range(500f,1000f), Random.Range(500f,1000f)));
		//at last disable this script as well
		this.gameObject.GetComponent<EnemyHealth> ().enabled = false;
	}

}

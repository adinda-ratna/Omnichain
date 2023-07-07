using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHealth : MonoBehaviour {
	
	//Ninja states
	public enum NinjaState{ALIVE,DIED};
	public static NinjaState ninjaState;

	//ninja's hitpoints
	public int hitPoint;

	int level;

	void Awake()
	{
		//set the ninja state to alive
		ninjaState = NinjaState.ALIVE;
	}

	void Start()
	{
		LocalData data= DatabaseManager.Instance.GetLocalData();

		
		hitPoint = PlayerPrefs.GetInt("MaxHitPoint",10);
		level = PlayerPrefs.GetInt("Level", 1);
		if (data != null)
        {
			hitPoint = data.MaxHitPoint;
			level = data.Level;


		}
		
		if (level % 7 == 0)
			hitPoint +=1;
		GameObject.Find ("ScoreUI").GetComponent<Score> ().UpdateHealth ();
	}

	//if ninja's hitpoint reaches to zero
	public void fall()
	{
		//blood particle system playes
		this.gameObject.GetComponentInChildren<ParticleSystem> ().Play ();

		//ninja state is seted to died
		ninjaState = NinjaState.DIED;

		audioManager.instance.PlaySound ("Die");

		//gets all body parts rigidbody
		Rigidbody2D[] Ninja = this.gameObject.GetComponentsInChildren<Rigidbody2D> ();

		//disable the attack animator
		Attack.attackObj.attack.enabled = false;

		//set all body parts body type to dynamic and gives force for fall effect 
		for (int i = 0; i < Ninja.Length; i++) 
		{
			Ninja [i].bodyType = RigidbodyType2D.Dynamic;
	
			if (Ninja [i].name == "NinjaArm1Left")
				Ninja [i].GetComponent<Rigidbody2D> ().AddForce (new Vector2( Random.Range(-1000f,-500f), Random.Range(500f,1000f)));
		}
	}

}

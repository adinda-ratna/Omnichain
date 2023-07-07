using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillNinja : MonoBehaviour {

	public int damage;

	bool hit = false;

	void Update()
	{
		if (Manager.State == Manager.gameState.PAUSE && !hit)
			this.gameObject.GetComponent<Rigidbody2D> ().simulated = false;
		else if(Manager.State == Manager.gameState.PLAY && !hit)
			this.gameObject.GetComponent<Rigidbody2D> ().simulated = true;
	}

	//if ninja collides
	void OnCollisionEnter2D(Collision2D info)
	{
		if (info.collider.gameObject.tag == "Hat") 
		{
			hit = true;

			HelmetManager.HelmetHitPoint -= 1;

			this.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
			this.GetComponent<Rigidbody2D> ().simulated = false;

			if (HelmetManager.HelmetHitPoint <= 0)
				info.gameObject.GetComponent<HelmetManager> ().DistroyHelmet ();

				//GameObject.FindWithTag ("Hat").GetComponent<HelmetManager> ().DistroyHelmet ();

			Destroy (this.gameObject,0.2f);
		}
		else if(info.collider.gameObject.tag == "Shield")
		{
			hit = true;

			Shield.ShieldHitPoint -= 1;

			audioManager.instance.PlaySound ("WeaponHit");

			this.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
			this.GetComponent<Rigidbody2D> ().simulated = false;

			if (Shield.ShieldHitPoint <= 0)
				info.gameObject.GetComponent<Shield> ().DestroyShield ();

			Destroy(this.gameObject,0.2f);
		}
		//if enemy's weapon collides to ninja
		else if (info.collider.gameObject.tag == "Ninja")  
		{
			hit = true;
			//decrease the hit point of ninja
			if(info.gameObject.GetComponentInParent<NinjaHealth> ().hitPoint >0)
				info.gameObject.GetComponentInParent<NinjaHealth> ().hitPoint -= damage;

			GameObject.Find ("ScoreUI").GetComponent<Score> ().UpdateHealth ();

			//stops the enemy's weapon
			this.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
			this.GetComponent<Rigidbody2D> ().simulated = false;

			//if collided ninja's hitpoints reaches to zero
			if (info.gameObject.GetComponentInParent<NinjaHealth> ().hitPoint <= 0)
				info.gameObject.GetComponentInParent<NinjaHealth> ().fall ();
			else
				audioManager.instance.PlaySound ("Hurt");

			//destroy the enemy wepon after 0.2sec if it is collided to ninja
			Destroy (this.gameObject,0.2f);
		}
	}

}

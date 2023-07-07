using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour {

	public float damage;

	bool hit = false;

	void Update()
	{
		if (Manager.State == Manager.gameState.PAUSE && !hit)
			this.gameObject.GetComponent<Rigidbody2D> ().simulated = false;
		else if(Manager.State == Manager.gameState.PLAY && !hit)
			this.gameObject.GetComponent<Rigidbody2D> ().simulated = true;
	}

	//if weapon collides
	void OnCollisionEnter2D(Collision2D info)
	{
		//if weapon collides to enemy
		if (info.collider.gameObject.tag == "Enemy") 
		{
			hit = true;
			if (info.collider.gameObject.name == "EnemyHead") 
			{
				info.gameObject.GetComponentInParent<EnemyHealth> ().hitPoint = 0;
				info.gameObject.GetComponentInParent<EnemyHealth> ().headShot = true;
			} 
			else 
			{
				info.gameObject.GetComponentInParent<EnemyHealth> ().hitPoint -= damage;
			}
			//stops the weapon
			this.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
			this.GetComponent<Rigidbody2D> ().simulated = false;
			this.transform.parent = info.transform;

			//if collided enemy's hitpoints reaches to zero
			if (info.gameObject.GetComponentInParent<EnemyHealth> ().hitPoint <= 0) 
			{
				//destory the plateform
				Destroy (info.collider.transform.parent.GetChild(9).gameObject);
				//runs fall method
				info.gameObject.GetComponentInParent<EnemyHealth> ().fall ();
			}
			else
				audioManager.instance.PlaySound ("Grunt");
			//after 0.3sec weapon gets distroy if it is collide to enemy
			Destroy (this.gameObject,0.3f);
		}

		//if weapon collides to enemy
		else if (info.collider.gameObject.tag == "Devil") 
		{
			hit = true;
			if (info.collider.gameObject.name == "DevilHead") 
				info.gameObject.GetComponentInParent<DevilHealth> ().hitPoint -= 3;
			else 
				info.gameObject.GetComponentInParent<DevilHealth> ().hitPoint -= 1;
			
			//stops the weapon
			this.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
			this.GetComponent<Rigidbody2D> ().simulated = false;
			this.transform.parent = info.transform;

			//if collided enemy's hitpoints reaches to zero
			if (info.gameObject.GetComponentInParent<DevilHealth> ().hitPoint <= 0) 
			{
				//destory the plateform
				Destroy (info.collider.transform.parent.GetChild(9).gameObject);
				//runs fall method
				info.gameObject.GetComponentInParent<DevilHealth> ().fall ();
			}
			else
				audioManager.instance.PlaySound ("Grunt");
			//after 0.3sec weapon gets distroy if it is collide to enemy
			Destroy (this.gameObject,0.3f);
		}

		else if (info.gameObject.tag == "Weapon") 
		{
			audioManager.instance.PlaySound ("WeaponHit");
		}
	}

}

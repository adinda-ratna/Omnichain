using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	//When ever anything collide the heart
	void OnCollisionEnter2D(Collision2D info)
	{
		//if colliding obj is ninja weapon
		if (info.gameObject.transform.tag == "Weapon") 
		{
			audioManager.instance.PlaySound ("PowerUp");
			GameObject.Find ("GameManager").GetComponent<Manager> ().increaseHealth ();
			Destroy (info.gameObject);
			Destroy (this.gameObject);
		}
	}
}

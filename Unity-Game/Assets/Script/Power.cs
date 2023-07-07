using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {

	public GameObject Shield;


	//calls when anything collides
	void OnCollisionEnter2D(Collision2D info)
	{
		//runs only if ninja weapon is hit
		if (info.gameObject.transform.tag == "Weapon") 
		{
			audioManager.instance.PlaySound ("PowerUp");
			Instantiate (Shield, Shield.transform.position,Shield.transform.rotation);
			Destroy (info.gameObject);
			Destroy (this.gameObject);
		}
	}
}


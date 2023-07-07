using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	//Destroys what ever collide to borders
	void OnCollisionEnter2D(Collision2D info)
	{
		//Destroys the wepoan
		if (info.collider.transform.tag == "Weapon")
			Destroy (info.collider.transform.gameObject);
		//Destroy the hat
		else if (info.collider.transform.tag == "Hat")
			Destroy (info.collider.transform.gameObject);
		//Destroys the enemies
		else if (info.collider.transform.parent.tag == "EnemyContainer") 
		{
			Destroy (info.collider.transform.parent.gameObject);

		}
		//Destroys the Devil
		else if (info.collider.transform.parent.tag == "DevilContainer")
		{
			Destroy (info.collider.transform.parent.gameObject);

		}

		//Destroys the ninja
		else if (info.collider.transform.parent.tag == "NinjaContainer") 
		{
			Destroy (info.collider.transform.parent.gameObject);

			//Set game state to gameover
			Manager.State = Manager.gameState.GAMEOVER;
		}
	}

}

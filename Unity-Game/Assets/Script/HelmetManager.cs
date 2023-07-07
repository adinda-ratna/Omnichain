using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetManager : MonoBehaviour {


	public static int HelmetHitPoint;

	public int hitPoint;
	//Set the Helmet Hitpoint
	void Start()
	{
		HelmetHitPoint = hitPoint;
	}
	//When Static int HelmetHitPoint reaches at 0
	public void DistroyHelmet()
	{
		//Unparent the hat and it's rigitbody type so that it fall down
		transform.parent = null;
		this.gameObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
	}

}

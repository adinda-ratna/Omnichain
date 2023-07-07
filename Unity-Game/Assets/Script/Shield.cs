using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public static int ShieldHitPoint;
	public int HitPoint;

	void Start()
	{
		ShieldHitPoint = HitPoint;
	}
	//when shield hitpoint is zero
	public void DestroyShield()
	{
		gameObject.GetComponent<ParticleSystem> ().Play ();
		this.GetComponent<SpriteRenderer> ().sprite = null;
		this.GetComponent<CircleCollider2D> ().isTrigger = true;
		Destroy (this.gameObject,3f);
	}
}

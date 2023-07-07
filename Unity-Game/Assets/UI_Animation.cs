using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Animation : MonoBehaviour {

	public float x, y, time;

	public bool pinch;

	void OnEnable ()
	{
		if (pinch)
			pinchAnim ();
	}

	void pinchAnim()
	{
		iTween.PunchScale (this.gameObject,iTween.Hash("x",x,"y",y,"time",time,"looptype",iTween.LoopType.loop));
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour {

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			ContinueBtn ();
	}

	//closes the panel
	public void ContinueBtn()
	{
		audioManager.instance.PlaySound ("Click");
		this.gameObject.SetActive (false);
	}

}

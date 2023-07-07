using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IAPManager : MonoBehaviour
{

	public static IAPManager instance;



	//const string SKU_AdRemove = "com.fss.removead";

	

	public GameObject BuyErrorPanel, BuySuccessfulPanel,PackageContainer, IAPBackBtn, IAPPanel;


	private void Start()
	{
		instance = this;

	
	}

	

	

	
	

	//call when 5000 gold package clicked
	


	//closes the buy error and successful panels
	public void BackBtn()
	{
		BuyErrorPanel.SetActive (false);
		BuySuccessfulPanel.SetActive (false);
		PackageContainer.SetActive (true);
		audioManager.instance.PlaySound ("Click");
	}




}
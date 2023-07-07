using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWeapon : MonoBehaviour {

	public GameObject NotEnoughMOneyUI, DiscriptionPanel,WeaponPanel;

	public Text  WeaponCostTxt, DamageTxt, RespawnTxt;

	int money, cost, value;
	string prefs;


	void Start ()
	{
		LocalData data = DatabaseManager.Instance.GetLocalData();

		//display the total gold
		money = data.coins;

		Web3_UIManager.Instance.SetCoinText();
	}


	//set the cost and PlayerPrefs value of the weapon
	public void setCostandVale (int weaponCost,int Value)
	{
		cost = weaponCost;
		value = Value;
		WeaponCostTxt.text = cost.ToString();
	}
	//Sets the Weapon discription and prefs value
	public void setWeaponKey(string prefsKey)
	{
		prefs = prefsKey;

		if (prefs == "4EdgeStar") 
		{
			DamageTxt.text = "Damage : 0.5";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (prefs == "5EdgeStar") 
		{
			DamageTxt.text = "Damage : 0.6";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (prefs == "6EdgeStar") 
		{
			DamageTxt.text = "Damage : 0.7";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (prefs == "8EdgeStar") 
		{
			DamageTxt.text = "Damage : 0.8";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (prefs == "SpikedBall") 
		{
			DamageTxt.text = "Damage : 0.9";
			RespawnTxt.text = "Respawn Time : 0.7s";
		}
		else if (prefs == "RangedSpike") 
		{
			DamageTxt.text = "Damage : 0.6";
			RespawnTxt.text = "Respawn Time : 0.5s";
		}
		else if (prefs == "RangedNeedle") 
		{
			DamageTxt.text = "Damage : 0.7";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (prefs == "Torpedo") 
		{
			DamageTxt.text = "Damage : 0.8";
			RespawnTxt.text = "Respawn Time : 0.4s";
		}
		else if (prefs == "Sai") 
		{
			DamageTxt.text = "Damage : 0.8";
			RespawnTxt.text = "Respawn Time : 0.3s";
		}
		else if (prefs == "Kunai") 
		{
			DamageTxt.text = "Damage : 0.9";
			RespawnTxt.text = "Respawn Time : 0.3s";
		}
		else if (prefs == "Knife") 
		{
			DamageTxt.text = "Damage : 1";
			RespawnTxt.text = "Respawn Time : 0.2s";
		}
	}

	public void BuyBtn()
	{
		LocalData data = DatabaseManager.Instance.GetLocalData();
		//If not enough money
		if (data.coins <= cost) 
		{
			NotEnoughMOneyUI.SetActive (true);
			audioManager.instance.PlaySound ("NoMoney");
		}
		else 
		{
			//Subtract the Money based on weapon cost
			
			data.coins -= cost;
			money -= cost;
		
			Web3_UIManager.Instance.SetCoinText();

			audioManager.instance.PlaySound ("MoneySpend");

			if (prefs == "4EdgeStar")
				data.W_4EdgeStar = 1;
			else if (prefs == "5EdgeStar")
				data.W_5EdgeStar = 1;
			else if (prefs == "6EdgeStar")
				data.W_6EdgeStar = 1;
			else if (prefs == "8EdgeStar")
				data.W_8EdgeStar = 1;
			else if (prefs == "SpikedBall")
				data.SpikedBall = 1;
			else if (prefs == "RangedSpike")
				data.RangedSpike = 1;
			else if (prefs == "RangedNeedle")
				data.RangedNeedle = 1;
			else if (prefs == "Torpedo")
				data.Torpedo = 1;
			else if (prefs == "Sai")
				data.Sai = 1;
			else if (prefs == "Kunai")
				data.Kunai = 1;
			else if (prefs == "Knife")
				data.Knife = 1;

			//set the bought weapon as default weapon
			data.Weapon = value;
			DatabaseManager.Instance.UpdateData(data);

			WeaponPanel.SetActive (true);
			DiscriptionPanel.SetActive (true);

			//disable the buy button
			GameObject.Find ("WeaponPanel").GetComponent<WeaponSelection> ().btnVisibility ();
			GameObject.Find ("WeaponPanel").GetComponent<WeaponSelection> ().updateSelectedWeaponTxt ();

			//update the gold. 
			GameObject.Find ("WeaponPanel").GetComponent<WeaponSelection> ().gold ();

			this.gameObject.SetActive (false);
		}
	}
	//closes the not enough money panel
	public void CloseBtn()
	{
		audioManager.instance.PlaySound ("Click");
		NotEnoughMOneyUI.SetActive (false);
	}
	//Cancel the but process
	public void CancelBtn()
	{
		audioManager.instance.PlaySound ("Click");
		DiscriptionPanel.SetActive (true);
		WeaponPanel.SetActive (true);
		this.gameObject.SetActive (false);
	}

}

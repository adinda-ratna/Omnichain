using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour {

	public GameObject[] SelectBtns, BuyBtns;
	public GameObject BuyUI, DiscriptionPanel,BackBtn,WeaponPanel;
	public Text DamageTxt, RespawnTxt, moneyTxt, unlockTxt;

	int IsWeaponUnlocked;

	void Start ()
	{
		
		SelectBtns = GameObject.FindGameObjectsWithTag("SelectBtn");

		BuyBtns = new GameObject[GameObject.FindGameObjectsWithTag("BuyBtn").Length];
		int j=0;
		for (int i = 0; i < this.gameObject.GetComponentsInChildren<Button> ().Length; i++) 
		{
			if (this.gameObject.GetComponentsInChildren<Button> () [i].tag == "BuyBtn") 
			{
				BuyBtns[j] = this.gameObject.GetComponentsInChildren<Button> ()[i].gameObject;
				j++;
			}
		}

		updateSelectedWeaponTxt ();
		btnVisibility ();

	}
	void OnDisable(){
		//Array.Clear(SelectBtns, 0, SelectBtns.Length);
		//Array.Clear(BuyBtns, 0, BuyBtns.Length);
	}

	bool first = true;
	void OnEnable(){
		gold ();

        if (first)
        {
			first = false;
			return;
        }
		btnVisibility();


	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			BackBtn.GetComponent<Button> ().onClick.Invoke ();			
		}
	}

	public void updateSelectedWeaponTxt()
	{
		Button SelectedWeaponBtn;

		for (int i = 0; i < SelectBtns.Length; i++) 
				SelectBtns [i].GetComponentInChildren<Text> ().text = "Select";


		LocalData data= DatabaseManager.Instance.GetLocalData();


		//Than Selected weapon's text is set to Selected
		int selectedWeapon = PlayerPrefs.GetInt ("Weapon",0);

		if(data!= null)
        {
			selectedWeapon = data.Weapon;

		}

		if (selectedWeapon == 0) 
		{
			SelectedWeaponBtn = GameObject.Find ("3EdgeStarBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.4";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (selectedWeapon == 1) 
		{
			SelectedWeaponBtn = GameObject.Find ("4EdgeStarBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.5";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (selectedWeapon == 2) 
		{
			SelectedWeaponBtn = GameObject.Find ("5EdgeStarBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.6";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (selectedWeapon == 3) 
		{
			SelectedWeaponBtn = GameObject.Find ("6EdgeStarBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.7";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (selectedWeapon == 4) 
		{
			SelectedWeaponBtn = GameObject.Find ("8EdgeStarBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.8";
			RespawnTxt.text = "Respawn Time : 0.6s";
		}
		else if (selectedWeapon == 5) 
		{
			SelectedWeaponBtn = GameObject.Find ("SpikedBallBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.9";
			RespawnTxt.text = "Respawn Time : 0.7s";
		}
		else if (selectedWeapon == 6) 
		{
			SelectedWeaponBtn = GameObject.Find ("RangedSpikeBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.6";
			RespawnTxt.text = "Respawn Time : 0.5s";
		}
		else if (selectedWeapon == 7) 
		{
			SelectedWeaponBtn = GameObject.Find ("RangedNeedleBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.7";
			RespawnTxt.text = "Respawn Time : 0.5s";
		}
		else if (selectedWeapon == 8) 
		{
			SelectedWeaponBtn = GameObject.Find ("TorpedoBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.8";
			RespawnTxt.text = "Respawn Time : 0.4s";
		}
		else if (selectedWeapon == 9) 
		{
			SelectedWeaponBtn = GameObject.Find ("SaiBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.8";
			RespawnTxt.text = "Respawn Time : 0.3s";
		}
		else if (selectedWeapon == 10) 
		{
			SelectedWeaponBtn = GameObject.Find ("KunaiBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.9";
			RespawnTxt.text = "Respawn Time : 0.3s";
		}
		else if (selectedWeapon == 11) 
		{
			SelectedWeaponBtn = GameObject.Find ("KnifeBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 1";
			RespawnTxt.text = "Respawn Time : 0.2s";
		}
		else if (selectedWeapon == 12)
		{
			SelectedWeaponBtn = GameObject.Find ("ColdSteel4EdgeStarBtn").GetComponent<Button>();
			SelectedWeaponBtn.GetComponentInChildren<Text> ().text = "Selected";
			DamageTxt.text = "Damage : 0.6";
			RespawnTxt.text = "Respawn Time : 0.4s";
		}

	}

	public void SetWeapon(int value)
	{
		PlayerPrefs.SetInt ("Weapon", value);
		audioManager.instance.PlaySound ("Click");
		updateSelectedWeaponTxt ();
	}

	public void BuyBtn(string prefsKey)
	{
		BuyUI.SetActive (true);
		WeaponPanel.SetActive (false);
		DiscriptionPanel.SetActive (false);
		audioManager.instance.PlaySound ("Click");
		CostandValue (prefsKey);
		GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setWeaponKey(prefsKey);
	}

	public void btnVisibility()	
	{

		LocalData data = DatabaseManager.Instance.GetLocalData();
		if (data == null) return;


		IsWeaponUnlocked = data.W_4EdgeStar;
		setVisibility (0);

		IsWeaponUnlocked = data.W_5EdgeStar;
		setVisibility (1);

		IsWeaponUnlocked = data.W_6EdgeStar;
		setVisibility (2);

		IsWeaponUnlocked = data.W_8EdgeStar;
		setVisibility (3);

		IsWeaponUnlocked = data.SpikedBall;
		setVisibility (4);

		IsWeaponUnlocked = data.RangedSpike;
		setVisibility (5);

		IsWeaponUnlocked = data.RangedNeedle;
		setVisibility (6);

		IsWeaponUnlocked = data.Torpedo;
		setVisibility (7);

		IsWeaponUnlocked = data.Sai;
		setVisibility (8);

		IsWeaponUnlocked = data.Kunai;
		setVisibility (9);

		IsWeaponUnlocked = data.Knife;
		setVisibility (10);

		IsWeaponUnlocked = data.ColdSteel4EdgeStar;

		if (IsWeaponUnlocked == 1) {
			unlockTxt.gameObject.SetActive (false);
			SelectBtns [12].gameObject.GetComponent<Button> ().interactable = true;
		} else {
			unlockTxt.gameObject.SetActive (true);
			SelectBtns [12].gameObject.GetComponent<Button> ().interactable = false;
		}
	}

	void setVisibility(int i)
	{
		if (IsWeaponUnlocked == 1) 
		{
			SelectBtns [i + 1].gameObject.SetActive (true);
			BuyBtns [i].gameObject.SetActive (false);
		}
		else 
		{
			SelectBtns [i + 1].gameObject.SetActive (false);
			BuyBtns [i].gameObject.SetActive (true);
		}
	}

	void CostandValue(string prefsKey)
	{
		//setCostandValue(Cost,Value)
		if (prefsKey == "4EdgeStar")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(1000,1);
		else if (prefsKey == "5EdgeStar")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(1500,2);
		else if (prefsKey == "6EdgeStar")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(2000,3);
		else if (prefsKey == "8EdgeStar")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(2500,4);
		else if (prefsKey == "SpikedBall")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(3500,5);
		else if (prefsKey == "RangedSpike")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(4000,6);
		else if (prefsKey == "RangedNeedle")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(4500,7);
		else if (prefsKey == "Torpedo")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(5500,8);
		else if (prefsKey == "Sai")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(7500,9);
		else if (prefsKey == "Kunai")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(10000,10);
		else if (prefsKey == "Knife")
			GameObject.Find("BuyUI").gameObject.GetComponent<BuyWeapon>().setCostandVale(12000,11);
	}

	public void gold()
	{
		Web3_UIManager.Instance.SetCoinText();
	}
}

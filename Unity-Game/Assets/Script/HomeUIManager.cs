using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Advertisements;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using Defective.JSON;
using System;


#if UNITY_IOS
	//using System.Runtime.InteropServices;
	using UnityEngine.SocialPlatforms;
#endif

public class HomeUIManager : MonoBehaviour
{

    public static HomeUIManager insta;

    public GameObject HomeUI, MainMenuUI, AchievementUI, SettingsUI, WeaponUI, StoreUI, HelmetUI, IAPUI, RewardPanel, NoAdVideoPanel, BG, RateScreen, regScreen, logBtn, RegBtn, lotOutBtn;
    public Image Highlight;

    public Sprite[] BackgroungSprite;

    public int rateEverylaunch;

    public Text MoneyTxt;

    private string restoreDatastring = null;

    public static int btnType;


#if UNITY_ANDROID
    string ANDROID_LINK = "https://play.google.com/store/apps/details?id=com.tgs.nssf";
#elif UNITY_IOS
		string IOS_LINK = "https://itunes.apple.com/app/id1441387403";
#endif

    void Awake()
    {
        insta = this;
    }

    //sets the camera size and background image size based on camera size and display ad banner
    void Start()
    {
        if (CoreWeb3Manager.Instance)
        {
            if (CoreWeb3Manager.Instance.isLogin) OpenMenu();
        }
        float horizontalResolution = 1920f;
        float currentAspect = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;

        SetBackGroundScale();


       
        Web3_UIManager.Instance.SetCoinText();

     
        checkAchievement();
        Achievement.instance.SetAchievements();

    }




    public void checkAchievement()
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        if (data != null)
        {
            if ((Achievement.instance.LevelSlider.value >= Achievement.instance.LevelSlider.maxValue && data.LevelAchievement != 3)
                || (Achievement.instance.KillSlider.value >= Achievement.instance.KillSlider.maxValue && data.KillAchievement != 3)
                || (Achievement.instance.HeadshotSlider.value >= Achievement.instance.HeadshotSlider.maxValue && data.HeadshotAchievement != 3)
                || (Achievement.instance.BossKillSlider.value >= Achievement.instance.BossKillSlider.maxValue && data.BossKillAchievement != 1))
            {
                Highlight.GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                Highlight.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
       
    }

    //loads the game scene
    public void PlayBtn()
    {
        audioManager.instance.PlaySound("Click");
        audioManager.instance.StopSound("HomeBackground");
        audioManager.instance.PlaySound("Background");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //open the store
    public void StoreBtn()
    {
        audioManager.instance.PlaySound("Click");
        HomeUI.SetActive(false);
        StoreUI.SetActive(true);

        BG.GetComponent<SpriteRenderer>().sprite = BackgroungSprite[1];
        SetBackGroundScale();
    }
    //goes to weapon store
    public void WeaponBtn()
    {
        audioManager.instance.PlaySound("Click");
        //disable the home ui and display the weapon ui
        StoreUI.SetActive(false);
        WeaponUI.SetActive(true);
    }
    //goes to hat store
    public void HelmetBtn()
    {
        audioManager.instance.PlaySound("Click");
        //disable the home ui and display the weapon ui
        StoreUI.SetActive(false);
        HelmetUI.SetActive(true);
    }

    public void AchievementBtn()
    {
        audioManager.instance.PlaySound("Click");
        MainMenuUI.SetActive(false);
        AchievementUI.SetActive(true);
    }

    //open google play or itune store for rating
    public void RateBtn()
    {
        audioManager.instance.PlaySound("Click");
#if UNITY_ANDROID
        Application.OpenURL(ANDROID_LINK);
        Debug.Log("Rate");
#elif UNITY_IOS
		Application.OpenURL(IOS_LINK);
#endif
    }

    public void closerateUs()
    {
        audioManager.instance.PlaySound("Click");
        RateScreen.SetActive(false);
    }

    //open game settings
    public void SettingsBtn()
    {
        audioManager.instance.PlaySound("Click");
        //disable the home ui and display the settings ui
        HomeUI.SetActive(false);
        SettingsUI.SetActive(true);
    }
    //close the application
    public void QuitBtn()
    {
        //audioManager.instance.PlaySound ("Click");
        Application.Quit();
        Debug.Log("Quit");
    }
    //closes the settings
    public void SettingsBackBtn()
    {
        audioManager.instance.PlaySound("Click");
        SettingsUI.SetActive(false);
        HomeUI.SetActive(true);
        Web3_UIManager.Instance.SetCoinText();
    }
    //closes the weapon store and hat store 
    public void BackBtn()
    {
        HomeUIManager.insta.saveData();
        audioManager.instance.PlaySound("Click");
        //disable the settings UI & weapon UI and display home UI 
        WeaponUI.SetActive(false);
        HelmetUI.SetActive(false);
        StoreUI.SetActive(true);
    }
    //comes back from store to home
    public void StoreBackBtn()
    {
        audioManager.instance.PlaySound("Click");
        HomeUI.SetActive(true);
        StoreUI.SetActive(false);
        Web3_UIManager.Instance.SetCoinText();
        BG.GetComponent<SpriteRenderer>().sprite = BackgroungSprite[0];
        SetBackGroundScale();
    }
    //closes the no ad panel
    public void NoAdPanelContinueBtn()
    {
        NoAdVideoPanel.SetActive(false);
    }
    //Play reward video
    public void RewardBtn()
    {
        audioManager.instance.PlaySound("Click");
            NoAdVideoPanel.SetActive(true);
            

        /*if (Advertisement.IsReady ("rewardedVideo")) 
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		} 
		else 
		{
			
		}*/
    }
    /*
	//check if ad played skipped or failed
	void HandleShowResult(ShowResult result)
	{
        
		switch (result) 
		{
			case ShowResult.Finished:
				Debug.Log ("Ad Played successfully");
				PlayerPrefs.SetInt ("Money",PlayerPrefs.GetInt("Money",50) + 50);
				MoneyTxt.text = "" + PlayerPrefs.GetInt ("Money",50);
				Debug.Log ("Reward" + PlayerPrefs.GetInt("Money"));
				Advertisement.Initialize ("2829189",true);
				RewardPanel.SetActive (true);
			break;

			case ShowResult.Skipped:
				
				Debug.Log ("Ad Skipped");
			break;

			case ShowResult.Failed:
				Debug.Log ("Fail to Load Ad");
				NoAdVideoPanel.SetActive (true);
			break;
		}
	}
    */
    //open IAP Panel
    public void IAPBtn()
    {
        audioManager.instance.PlaySound("Click");
        IAPUI.SetActive(true);
        HomeUI.SetActive(false);
        //BG.GetComponent<SpriteRenderer>().sprite = BackgroungSprite[1];
        //SetBackGroundScale ();
    }
    //closes IAP panel
    public void IAPBackBtn()
    {
        audioManager.instance.PlaySound("Click");
        IAPUI.SetActive(false);
        HomeUI.SetActive(true);
        Web3_UIManager.Instance.SetCoinText();
        Debug.Log("IAP");
        BG.GetComponent<SpriteRenderer>().sprite = BackgroungSprite[0];
    }
    //set the sharing text of app
    public void ShareBtn()
    {
        audioManager.instance.PlaySound("Click");
    
    }
    //set the background image size based on screen size
    void SetBackGroundScale()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        Sprite s = BG.GetComponent<SpriteRenderer>().sprite;

        float unitWidth = s.textureRect.width / s.pixelsPerUnit;
        float unitHeight = s.textureRect.height / s.pixelsPerUnit;

        BG.transform.localScale = new Vector3(width / unitWidth, height / unitHeight) + new Vector3(0.02f, 0.02f, 0.02f);
    }


    //== game data save on server methods ==//



    public InputField username, pass1, pass2;
    public Text infoText;
    public GameObject inputPassF2;
    private string uname, upass;

    //reg or login
    public void openRegScreen(int myType)
    {
        btnType = myType; //set screen type
        if (btnType == 1)
        { //login
            infoText.text = "Enter username and password to login";
            inputPassF2.SetActive(false);
        }
        else if (btnType == 2)
        { // register
            infoText.text = "Enter username and password to register";
            inputPassF2.SetActive(true);
        }
        regScreen.SetActive(true);
    }

    public void closeRegScreen()
    {
        regScreen.SetActive(false);
    }

    public void logOut()
    {
        PlayerPrefs.SetInt("login", 0);
        PlayerPrefs.SetString("uname", "");
        PlayerPrefs.SetString("upass", "");
        PlayerPrefs.Save();
        logBtn.SetActive(true);
        RegBtn.SetActive(true);
        lotOutBtn.SetActive(false);
    }

    public void submitBtn()
    {
        if (btnType == 1)
        { //login
            if (!string.IsNullOrEmpty(username.text) && !string.IsNullOrEmpty(pass1.text))
            {
                uname = username.text;
                upass = pass1.text;
                StartCoroutine(loginData());
            }
            else
            {
                infoText.text = "Error ! Check username or password";
            }
        }
        else if (btnType == 2)
        { // register
            if (username.text.Length < 4 || pass1.text.Length < 4)
            {
                infoText.text = "Error !\nNeed minimum 4 letter of username and password";
            }
            else if (!string.IsNullOrEmpty(username.text.ToString()) && !string.IsNullOrEmpty(pass1.text.ToString()) && pass1.text.ToString().CompareTo(pass2.text.ToString()) == 0)
            {
                uname = username.text;
                upass = pass1.text;
                StartCoroutine(registerUser());
            }
            else
            {
                infoText.text = "Error ! Check username or password";
            }
        }
    }

    // save date
   

    // register upload data




}

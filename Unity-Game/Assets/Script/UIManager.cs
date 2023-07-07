using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//using UnityEngine.Advertisements;

public class UIManager : MonoBehaviour
{
    public static UIManager insta;
    public GameObject PauseUI, GameOverUI, SettingsUI, ScoreUI, LevelCompleteUI, SkippedPanel, FailedPanel, CanNotContinuePanel, Ninja;
    [SerializeField] GameObject claimTokenBTN;

    public Text HighScoreTxt, ScoreTxt;


    public bool IsInterstitialDisplayed, AdWatched;

    private void Awake()
    {
        insta = this;
    }

    void Start()
    {
        IsInterstitialDisplayed = false;
        AdWatched = false;


        initializeVideoAd();

    }

    void initializeVideoAd()
    {
        /*
#if UNITY_ANDROID
        Advertisement.Initialize("2888015");
#elif UNITY_IOS
		Advertisement.Initialize ("2888016");
#endif*/
    }



    void Update()
    {
#if UNITY_ANDROID
        //Enable and disable PauseUI and GamePlayUI using Escape key
        if (Input.GetKeyDown(KeyCode.Escape) && Manager.State != Manager.gameState.GAMEOVER && Manager.State != Manager.gameState.LEVELUPGRADE)
        {
            audioManager.instance.PlaySound("Click");
            SettingsUI.SetActive(false);
            PauseUI.SetActive(!PauseUI.activeSelf);
            if (PauseUI.activeSelf)
            {
                //if (PlayerPrefs.GetInt ("Inerstitial") == 1) 
                //{
                if (!IsInterstitialDisplayed)
                {
                    AdEvent.insta.showAd();
                    IsInterstitialDisplayed = true;
                }
                //}
                Manager.State = Manager.gameState.PAUSE;
                ScoreUI.SetActive(false);
            }
            else
            {
                //if (PlayerPrefs.GetInt ("Inerstitial") == 1)
                //{
                // AdEvent.insta.initializeInerstitial();
                IsInterstitialDisplayed = false;
                //}
                Manager.State = Manager.gameState.PLAY;
                ScoreUI.SetActive(true);
                Invoke("activeAttack", 0.5f);
            }

        }
#endif

        //if GameOver
        if (Manager.State == Manager.gameState.GAMEOVER)
        {
            LocalData data = DatabaseManager.Instance.GetLocalData();
            

            ScoreUI.SetActive(false);
            if (data != null)
            {
                HighScoreTxt.text = "Best : " + data.HighScore;
            }
            ScoreTxt.text = "Score : " + Score.score;
            GameOverUI.SetActive(true);

            //if (PlayerPrefs.GetInt ("Inerstitial", 1) == 1) 
            //{
            
            //}
        }
    }

    public void ResumeBtn()
    {
        audioManager.instance.PlaySound("Click");

        //if (PlayerPrefs.GetInt ("Inerstitial") == 1)
        //{
        // AdEvent.insta.initializeInerstitial();
        IsInterstitialDisplayed = false;
        //}

        PauseUI.SetActive(false);
        ScoreUI.SetActive(true);
        Manager.State = Manager.gameState.PLAY;
        Invoke("activeAttack", 0.5f);
    }

    //enable the ninja to attack again after resume
    void activeAttack()
    {
        Attack.attackObj.fire = true;
    }

    //Load Home Scene
    public void HomeBtn()
    {
        audioManager.instance.PlaySound("Click");
        audioManager.instance.StopSound("Background");
        audioManager.instance.PlaySound("HomeBackground");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //Reload the Scene
    public void RetryBtn()
    {
        audioManager.instance.PlaySound("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Score.score = 0;
    }

    //Enable SettingsUI
    public void SettingsBtn()
    {
        audioManager.instance.PlaySound("Click");
        PauseUI.SetActive(false);
        SettingsUI.SetActive(true);
    }

    //Disable SettingsUI and Back to PauseUI
    public void BackBtn()
    {
        audioManager.instance.PlaySound("Click");
        SettingsUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    //Pause the Game
    public void PauseBtn()
    {
        audioManager.instance.PlaySound("Click");

        //if (PlayerPrefs.GetInt ("Inerstitial") == 1)
        //{
        IsInterstitialDisplayed = true;
        //}
        ScoreUI.SetActive(false);
        PauseUI.SetActive(true);
        Manager.State = Manager.gameState.PAUSE;
    }

    //Continue the game After Level Complete
    public void ContinueBtn()
    {
        Manager.State = Manager.gameState.PLAY;
        LevelCompleteUI.SetActive(false);
        ScoreUI.SetActive(true);
        Invoke("activeAttack", 1f);
    }

    public void GameOverContinueBtn()
    {
        if (!AdWatched)
        {
            /*
			if (Advertisement.IsReady ("rewardedVideo"))
			{
				var options = new ShowOptions { resultCallback = HandleShowResult };
				Advertisement.Show ("rewardedVideo", options);
			}
			else
			{
				
				initializeVideoAd ();
			}*/
           
        }
        else
            CanNotContinuePanel.SetActive(true);
    }

    /*
	void HandleShowResult(ShowResult result)
	{
		switch (result) 
		{
		case ShowResult.Finished:
			AdWatched = true;
			Debug.Log ("Ad Played successfully");

			Manager.State = Manager.gameState.PLAY;
			ScoreUI.SetActive (true);
			Instantiate (Ninja, Ninja.transform.position, Ninja.transform.rotation);

			IsInterstitialDisplayed = false;
			AdEvent.insta.initializeInerstitial();
			GameOverUI.SetActive (false);
		break;

		case ShowResult.Skipped:
			Debug.Log ("Ad Skipped");
			initializeVideoAd ();
			SkippedPanel.SetActive (true);
		break;

		case ShowResult.Failed:
			Debug.Log ("Fail to Load Ad");
			initializeVideoAd ();
			FailedPanel.SetActive (true);
		break;
		}
	}
    */
    public void CloseBtn()
    {
        SkippedPanel.SetActive(false);
        FailedPanel.SetActive(false);
        CanNotContinuePanel.SetActive(false);
    }
}

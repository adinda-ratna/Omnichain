using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    //Static int score variable so that it can be accessed by enemy when it dies
    public static int score, moneyIncrement;

    public GameObject LevelCompleteUI, LCPanel, Devil;
    [SerializeField] GameObject claimTokenBTN;

    //UI Texts
    public Slider HealthBar;
    public Text scoreTxt, moneyTxt, moneyIncrementTxt, scoreIncrementTxt, healthIncrementTxt, TargetKillTxt, TargetHeadShotTxt, LevelTxt, BossTxt;

    private int level, targetKill, targetHeadShot;


    void Start()
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        if (data == null) return;

        //default score 0
        score = 0;
        //sets score text to 0 at start
        scoreTxt.text = "Kill : " + score;
        //sets Money text stored in prefs
        Web3_UIManager.Instance.SetCoinText();
        HealthBar.maxValue = data.MaxHitPoint;
        HealthBar.value = HealthBar.maxValue;

        setLevel();
        setTarget();
    }



    public void UpdateScore()
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        if (data == null) return;
        //updates the score txt
        scoreTxt.text = "Kill : " + score;
        //updates the money txt
        Web3_UIManager.Instance.SetCoinText();

        moneyIncrementTxt.text = "+" + moneyIncrement;
        moneyIncrementTxt.gameObject.SetActive(true);
        iTween.ScaleFrom(moneyIncrementTxt.gameObject, iTween.Hash("scale", new Vector3(0f, 0f, 0f), "time", 1f));

        scoreIncrementTxt.gameObject.SetActive(true);
        iTween.ScaleFrom(scoreIncrementTxt.gameObject, iTween.Hash("scale", new Vector3(0f, 0f, 0f), "time", 1f));

        Invoke("disableIncreamentTxt", 1.3f);


       
        //if current score is greater than all time best score than is stored as new all time best score
        if (data.HighScore < score)
        {
            data.HighScore = score;
            DatabaseManager.Instance.UpdateData(data);
        }

    }

    public void UpdateHealth()
    {
        HealthBar.value = GameObject.FindWithTag("NinjaContainer").GetComponentInParent<NinjaHealth>().hitPoint;
    }

    public void displayHealthIncreaseTxt(int h)
    {
        healthIncrementTxt.text = "+ " + h;
        healthIncrementTxt.gameObject.SetActive(true);
        iTween.ScaleFrom(healthIncrementTxt.gameObject, iTween.Hash("scale", new Vector3(0f, 0f, 0f), "time", 1f));
        Invoke("disableIncreamentTxt", 1f);
    }

    void disableIncreamentTxt()
    {
        moneyIncrementTxt.gameObject.SetActive(false);
        scoreIncrementTxt.gameObject.SetActive(false);
        healthIncrementTxt.gameObject.SetActive(false);
        moneyIncrementTxt.rectTransform.localScale = new Vector3(0.45f, 0.45f, 1f);
        scoreIncrementTxt.rectTransform.localScale = new Vector3(0.5f, 0.5f, 1f);
        healthIncrementTxt.rectTransform.localScale = new Vector3(0.65f, 0.65f, 1f);
    }

    void setLevel()
    {
        LocalData data= DatabaseManager.Instance.GetLocalData();
        HealthBar.maxValue = PlayerPrefs.GetInt("MaxHitPoint", 10);

        level = PlayerPrefs.GetInt("Level", 1);
        if (data != null)
        {
            level = data.Level;
            HealthBar.maxValue = data.MaxHitPoint;
        }
        //audioManager.instance.LogScreen("Level : " + level);
        LevelTxt.text = "LEVEL : " + level;

        //if (level % 5 == 0) {
        //	HealthBar.maxValue += 1;
        //}
       
        UpdateHealth();
    }

    void setTarget()
    {
        targetKill = level * 3;
        TargetKillTxt.text = "Kill : " + targetKill;

        targetHeadShot = level * 2;
        TargetHeadShotTxt.text = "HeadShot : " + targetHeadShot;
    }

    public void updateTargetKill()
    {
        if (targetKill > 0 && Manager.State == Manager.gameState.PLAY)
        {
            targetKill -= 1;
            TargetKillTxt.text = "Kill : " + targetKill;

            if (targetKill == 0 && targetHeadShot == 0)
                checkLevel();
        }
    }

    public void updateTargetHeadShot()
    {
        if (targetHeadShot > 0 && Manager.State == Manager.gameState.PLAY)
        {
            targetHeadShot -= 1;
            TargetHeadShotTxt.text = "HeadShot : " + targetHeadShot;

            if (targetKill == 0 && targetHeadShot == 0)
                checkLevel();
        }
    }

    void checkLevel()
    {

        if (level % 3 == 0)
        {
            BossTxt.gameObject.SetActive(true);
            iTween.ScaleFrom(BossTxt.gameObject, iTween.Hash("scale", new Vector3(0.2f, 0.2f, 1f), "time", 2f));
            Invoke("disableBossTxt", 2.5f);
            Instantiate(Devil, Devil.transform.position, Devil.transform.rotation);
            checkDevil();
        }
        else
            updateLevel();
    }

    void updateLevel()
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();

        level += 1;

        
        data.Level += 1;
      
        //PlayerPrefs.SetInt ("TargetKill",PlayerPrefs.GetInt ("Level") *(int) 3);

        setLevel();
        setTarget();

        if (level % 5 == 0)
        {
            data.MaxHitPoint+= 1;
        }
        DatabaseManager.Instance.UpdateData(data);

        Manager.State = Manager.gameState.LEVELUPGRADE;
        Manager.Stage = 1;
        Manager.NumOfEnemy = 1;

        LevelCompleteUI.SetActive(true);
        claimTokenBTN.SetActive(true);

        Invoke("DisableScoreUI", 0.7f);

        //if(PlayerPrefs.GetInt("Inerstitial") == 1)
        Invoke("ShowInterstitial", 0.5f);
    }

    void DisableScoreUI()
    {
        this.gameObject.SetActive(false);
    }

    void checkDevil()
    {
        if (GameObject.FindWithTag("DevilContainer") == null)
        {
            updateLevel();
            return;
        }
        Invoke("checkDevil", 0.5f);
    }

    void ShowInterstitial()
    {
        //	Invoke ("initializeInerstitial",0.5f);
    }

    void initializeInerstitial()
    {
        //AdEvent.insta.initializeInerstitial();
    }

    void disableBossTxt()
    {
        BossTxt.gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Web3_UIManager : MonoBehaviour
{
    #region Sinleton
    public static Web3_UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion




    public string username;



    public void ToggleStartUI(bool enabled)
    {
        start_ui.SetActive(enabled);
    }
    public void UpdateUserName(string _ethad = null)
    {
        if (_ethad != null)
        {
            usernameText.text = "" + _ethad;
        }
    }
    internal void UpdateStatus(string status)
    {
        Debug.Log(status);
        //throw new NotImplementedException();
    }









    #region Data Refresh UI SHOW
    public void UpdatePlayerUIData(bool _show, LocalData data, bool _init = false)
    {
        if (_show)
        {
            //scoreTxt.text = data.coins.ToString();
            // if (PhotonNetwork.LocalPlayer.CustomProperties["health"] != null) healthSlider.value = float.Parse(PhotonNetwork.LocalPlayer.CustomProperties["health"].ToString());
        }
    }
    #endregion

    #region Panel Management
    [Space(20f)]
    [Header("Panels")]
    [SerializeField] GameObject login_ui;
    [SerializeField] GameObject start_ui;


    [Header("Buttons")]
    [SerializeField] GameObject loginui_btns;

    public void StartGame()
    {
        //start_ui.SetActive(false);
        //StartUI.SetActive(false);


        //MPNetworkManager.insta.OnConnectedToServer();

        //MPNetworkManager.insta.OnConnectedToServer();
    }
    #endregion



    [Space(20f)]
    [Header("Informaion (Login)")]
    [SerializeField] TMP_Text usernameText;
    [SerializeField] TMP_Text statusText;






    #region Coin Texts
    [SerializeField] TMP_Text[] coin_texts;
    [SerializeField] TMP_Text[] token_texts;
    public void SetCoinText()
    {
        if (DatabaseManager.Instance.GetLocalData() != null)
        {
            int coins = DatabaseManager.Instance.GetLocalData().coins;
            for (int i = 0; i < coin_texts.Length; i++)
            {
                coin_texts[i].text = coins.ToString();
            }
        }
    }
    public void SetTokenBalanceText()
    {
        for (int i = 0; i < token_texts.Length; i++)
        {
            token_texts[i].text = CoreWeb3Manager.userTokenBalance;
        }
    }
    #endregion

    #region Food Delivered UI

    int reward;

    public void ClaimCoins()
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        data.coins += reward;
        DatabaseManager.Instance.UpdateData(data);

        SetCoinText();

    }
    public void ClaimToken()
    {
        CoreWeb3Manager.Instance.getDailyToken();
    }



    #endregion




    #region Token
    public void getToken()
    {
        if (CoreWeb3Manager.Instance)
        {
            CoreWeb3Manager.Instance.getDailyToken();
        }
    }
    #endregion



}

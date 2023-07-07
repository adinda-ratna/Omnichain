using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public static MessageBox insta;
    [SerializeField] GameObject msgBoxUI;
    [SerializeField] GameObject okBtn;
    [SerializeField] TMP_Text msgText;



    private void Awake()
    {
        insta = this;
        msgBoxUI.SetActive(false);
    }
    public void showMsg(string _msg, bool showBtn)
    {
        StopAllCoroutines();

        msgBoxUI.SetActive(true);
        if (showBtn) okBtn.SetActive(true);
        else okBtn.SetActive(false);

        msgText.text = _msg;

        StartCoroutine(WaitToShowOk());
    }


    IEnumerator WaitToShowOk()
    {
        if (!okBtn.activeSelf)
        {
            yield return new WaitForSeconds(40);
            okBtn.SetActive(true);
        }

    }

    public void OkButton()
    {
        StopAllCoroutines();
        msgBoxUI.SetActive(false);
    }

    internal void hideMsg()
    {
        msgBoxUI.SetActive(false);
    }


    [Header("Informaion (InGame)")]
    [SerializeField] GameObject information_box;
    [SerializeField] TMP_Text information_text;
    [SerializeField] Image information_image;
    Coroutine info_coroutine;
    public void ShowInformationMsg(string msg, float time, Sprite image = null)
    {
        if (image != null)
        {
            information_image.sprite = image;
            information_image.gameObject.SetActive(true);
        }
        else
        {
            information_image.gameObject.SetActive(false);
        }

        information_text.text = msg;

        if (info_coroutine != null)
        {
            StopCoroutine(info_coroutine);
        }

        info_coroutine = StartCoroutine(disableInformationMsg(time));
    }
    IEnumerator disableInformationMsg(float time)
    {
        LeanTween.cancel(information_box);

        information_box.SetActive(true);
       // LeanTween.scaleY(information_box, 1, 0.15f).setFrom(0).setIgnoreTimeScale(true);
        // AudioManager.Instance.playSound(0);

        yield return new WaitForSecondsRealtime(time);
        information_box.SetActive(false);
       /* LeanTween.scaleY(information_box, 0, 0.15f).setIgnoreTimeScale(true).setOnComplete(() => {
            information_box.SetActive(false);
        });*/
    }
}

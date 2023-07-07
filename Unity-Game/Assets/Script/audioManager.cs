using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{

    //Store Clip Name
    public string name;
    //Store Clip
    public AudioClip clip;
    //Clip Volume in Range 0 - 1
    [Range(0f, 1f)]
    public float volume = 0.7f;
    //Clip Pitch in Range 0.5 - 1.5
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;
    //Repeat clip or not
    public bool loop = false;

    public AudioMixerGroup MixerGroup;

    private AudioSource source;

    //Gives Clip Source to the Audio Source which to play
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.outputAudioMixerGroup = MixerGroup;
    }

    //Plays the Clip
    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}

public class audioManager : MonoBehaviour
{

    public static audioManager instance;

    public bool fbAnalitic, enableGoogleAnalytics;
    public string GoogleAnalyticsId_Android, GoogleAnalyticsId_IOS;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {

        Application.targetFrameRate = 60;

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


        /*if (fbAnalitic)
            initFacebookSDK();*/
        //LogScreen("GameStart");
    }

    void Start()
    {
        //sets all sounds clip
        for (int i = 0; i < sounds.Length; i++)
        {

            GameObject _go = new GameObject("Sound_ " + i + " _" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());

        }
        //play the background clip
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            PlaySound("HomeBackground");
        }



    }
    //finds the clip and play that
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            //if clip found than plays that clip and return
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.Log("No Sound");
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            //if clip found than stops that clip and return
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.Log("No Sound");
    }

    //////// FACEBOOK ANALITICS //////////
  

   






}

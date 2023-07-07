using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsUI : MonoBehaviour {

	public GameObject BackBtn;

	public Toggle MusicToggle, SoundsToggle;

	//used to store screen resolutions of device
	Resolution[] resolution;

	public AudioMixerGroup master, music, sounds;

	//used to store screen Graphics of device
	string[] graphics;

	void Start ()
	{
		init ();
	}

	void init()
	{
		if (PlayerPrefs.GetInt ("Music", 1) == 1)
			MusicToggle.isOn = true;
		else
			MusicToggle.isOn = false;

		if (PlayerPrefs.GetInt ("Sounds", 1) == 1)
			SoundsToggle.isOn = true;
		else
			SoundsToggle.isOn = false;
	}

	#if UNITY_ANDROID
	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Escape))
			BackBtn.GetComponent<Button> ().onClick.Invoke ();
	}
	#endif

	public void SetSounds()
	{
		if (SoundsToggle.isOn)
		{
			PlayerPrefs.SetInt ("Sounds", 1);
			sounds.audioMixer.SetFloat ("Sounds", 5f);
		}
		else 
		{
			PlayerPrefs.SetInt ("Sounds", 0);
			sounds.audioMixer.SetFloat ("Sounds",-80f);
		}
	}

	public void SetMusic()
	{
		if (MusicToggle.isOn) 
		{
			PlayerPrefs.SetInt ("Music", 1);
			music.audioMixer.SetFloat ("Music", 5);

			if(SceneManager.GetActiveScene().buildIndex == 0)
			 	audioManager.instance.PlaySound ("HomeBackground");
			else
				audioManager.instance.PlaySound ("Background");
		} 
		else 
		{
			PlayerPrefs.SetInt ("Music", 0);
			music.audioMixer.SetFloat ("Music",-80f);
		}
	}

}

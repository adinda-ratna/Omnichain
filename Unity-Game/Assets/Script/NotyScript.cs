using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using Assets.SimpleAndroidNotifications;
using System;

public class NotyScript : MonoBehaviour
{

    public float PlayReminderValue, PlayReminderValue2;
    public string ReminderTitle, ReminderMessage;
    void Start()
    {
        PlayReminderValue = PlayReminderValue * 60 * 24 * 60;
        PlayReminderValue2 = PlayReminderValue2 * 60 * 24 * 60;

#if UNITY_ANDROID
        NotificationManager.CancelAll();
#else
		LocalNotification.ClearNotifications ();
		LocalNotification.CancelNotification (2);
		LocalNotification.CancelNotification (1);
#endif

        NotificationSender();
    }

    public void NotificationSender()
    {
#if UNITY_ANDROID
        NotificationManager.CancelAll();
#else
		LocalNotification.ClearNotifications ();
		LocalNotification.CancelNotification (2);
		LocalNotification.CancelNotification (1);
#endif

        // Day Reminder 
        //reminder 1
        Debug.Log("Reminder Notification Send");
        int TimespanValueForRemider = (int)PlayReminderValue * 1000;

#if UNITY_ANDROID
        NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(TimespanValueForRemider / 1000), ReminderTitle, ReminderMessage, new Color(1, 0.3f, 0.15f), NotificationIcon.Bell);
#else
		LocalNotification.SendNotification(1, TimespanValueForRemider, ReminderTitle, ReminderMessage, new Color32(0xff, 0x44, 0x44, 255), true, true, false, "app_icon");
      
#endif

        //reminder 2
        int TimespanValueForRemider2 = (int)PlayReminderValue2 * 1000;

#if UNITY_ANDROID
        NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(TimespanValueForRemider2 / 1000), ReminderTitle, ReminderMessage, new Color(1, 0.3f, 0.15f), NotificationIcon.Bell);
#else
		LocalNotification.SendNotification(2, TimespanValueForRemider2, ReminderTitle, ReminderMessage, new Color32(0xff, 0x44, 0x44, 255), true, true, false, "app_icon");
#endif

    }
    void OnApplicationQuit()
    {

        NotificationSender();


    }


#if UNITY_ANDROID
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Debug.Log("un paused android");
#if UNITY_ANDROID
            NotificationManager.CancelAll();
#else
		LocalNotification.ClearNotifications ();
		LocalNotification.CancelNotification (2);
		LocalNotification.CancelNotification (1);
#endif
        }
        else
        {
            Debug.Log("paused android");
            NotificationSender();
        }
    }
#endif

#if UNITY_EDITOR || UNITY_IOS 
    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("paused ios");
            NotificationSender();
        }
        else
        {
            Debug.Log("un paused ios");
#if UNITY_ANDROID
            NotificationManager.CancelAll();
#else
		LocalNotification.ClearNotifications ();
		LocalNotification.CancelNotification (2);
		LocalNotification.CancelNotification (1);
#endif
        }

    }
#endif


}




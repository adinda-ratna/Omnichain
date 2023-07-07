using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementsUI : MonoBehaviour
{
    [SerializeField] Achievement achievement;

    private void OnEnable()
    {
        achievement.SetAchievements();
    }    
    private void Start()
    {
        
    }
}

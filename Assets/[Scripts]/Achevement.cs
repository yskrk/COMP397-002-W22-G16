using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achevement : Observer
{
    // Start is called before the first frame update
    public void Start()
    {
        PlayerPrefs.DeleteAll();

        // register point in scene
        foreach (var p in FindObjectsOfType<Point>())
        {
            p.RegisterObserver(this);
        }
    }

    public override void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.AchievementUnlocked)
        {
            string achevementKey = "Achivement - " + value;

            // Do nothing if player unlocked achievement
            if (PlayerPrefs.GetInt(achevementKey) == 1)
            {
                return;
            }

            PlayerPrefs.SetInt(achevementKey, 1);
            
            // below should be show in display
            Debug.Log(value + " Unlocked!");
        }
    }
}

public enum NotificationType
{
    AchievementUnlocked
}

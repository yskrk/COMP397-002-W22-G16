using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : Subject
{
    [SerializeField] private string pName;

    // gettin MedKit
    private void OnTriggerEnter(Collider other)
    {
        Notify(pName, NotificationType.AchievementUnlocked);
    }

    // launching by shotgun
    public void isShootWihtJump()
    {
        Notify(pName, NotificationType.AchievementUnlocked);
    }
}

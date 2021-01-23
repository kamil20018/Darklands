using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int xpToNextLevel, baseXpToNextLevel = 100;
    public static int currentXp = 0, level = 1;
    void Start()
    {
        xpToNextLevel = baseXpToNextLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentXp > xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Debug.Log("xp before level: " + currentXp);
        currentXp -= xpToNextLevel;
        level += 1;
        Debug.Log("level: " + level);
        Debug.Log("currXp: " + currentXp);
        SetXpToNextLevel();
    }

    private void SetXpToNextLevel()
    {
        xpToNextLevel = level * baseXpToNextLevel;
    }
}

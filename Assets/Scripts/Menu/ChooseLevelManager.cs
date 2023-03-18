using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int completeLevelIndex = PlayerPrefs.GetInt("complete-level");
        LevelTab[] levelTabs = GetComponentsInChildren<LevelTab>();

        for (int i = 0; i < levelTabs.Length; i++)
        {
            levelTabs[i].DisplayLevelTab(i+1, completeLevelIndex);
        }
    }

   
}

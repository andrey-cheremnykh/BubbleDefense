using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelManager : MonoBehaviour
{
    [SerializeField] GameObject levelTabPrefab;
    [SerializeField] int levelCount = 10;
    [SerializeField] Transform content;
    // Start is called before the first frame update
    void Start()
    {
        int completeLevelIndex = PlayerPrefs.GetInt("complete-level");

        for (int i = 0; i < levelCount; i++)
        {
            GameObject newTab = Instantiate(levelTabPrefab, content);
            newTab.GetComponent<LevelTab>().DisplayLevelTab(i + 1, completeLevelIndex);
        }
    }

   
}

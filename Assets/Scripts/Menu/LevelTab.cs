using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTab : MonoBehaviour
{
    Button playLevelButton;

    [SerializeField] Sprite starIcon, unlockedIcon, lockedIcon;

    [Space]
    [SerializeField] Image infoLevelIcon;
    [SerializeField] TMP_Text levelText;

    int levelIndex;

    // Start is called before the first frame update
    void Awake()
    {
        playLevelButton = GetComponentInChildren<Button>();
    }

    public void DisplayLevelTab(int index, int passedLevelIndex)
    {
        levelText.text = "Level " + index;
        if (index <= passedLevelIndex)
            infoLevelIcon.sprite = starIcon;
        else if (index == passedLevelIndex + 1)
            infoLevelIcon.sprite = unlockedIcon;
        else
        {
            infoLevelIcon.sprite = lockedIcon;
            playLevelButton.interactable = false;
        }

        levelIndex = index;
        playLevelButton.onClick.AddListener(LoadLevel);
    }

    void LoadLevel()
    {
        StartCoroutine(FindObjectOfType<TransitionLogic>().LoadLevel(levelIndex));
    }
   
}

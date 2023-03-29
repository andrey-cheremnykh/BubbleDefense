using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLogic : MonoBehaviour
{
    [SerializeField] GameObject mainPanel, upgradesPanel, levelsPanel;
    [SerializeField] GameObject fadePanel;

    bool isLoadingNewLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<FadePanel>().Hide();
        mainPanel.SetActive(true);
        upgradesPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }

    public void GoToUpgrades()
    {
        if (isLoadingNewLevel) return;
        mainPanel.SetActive(false);
        upgradesPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

    public void GoToLevels()
    {
        if (isLoadingNewLevel) return;
        mainPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void GoBackMenu()
    {
        if (isLoadingNewLevel) return;
        mainPanel.SetActive(true);
        upgradesPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }


    public void ContinueGame()
    {
        int completeIndex = PlayerPrefs.GetInt("complete-level");
        StartCoroutine(LoadLevel(completeIndex+1));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        if (isLoadingNewLevel) yield break;
        isLoadingNewLevel = true;
        fadePanel.GetComponent<FadePanel>().Show();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }

}

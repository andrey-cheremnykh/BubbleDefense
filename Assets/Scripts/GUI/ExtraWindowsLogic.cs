using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum GameStates
{
    NORMAL,
    PAUSE,
    WIN,
    GAMEOVER,
    TRANSITION
}

public class ExtraWindowsLogic : MonoBehaviour
{
    [SerializeField] GameObject gameOverWindow;
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject pauseWindow;
    [SerializeField] GameObject fadePanel;

    GameStates state = GameStates.NORMAL;

    float adsMult = 2;
    bool isAdLoaded = false;
    RewardedAd rewardedAd;

    // Start is called before the first frame update
    void Start()
    {
        rewardedAd = FindObjectOfType<RewardedAd>();
        gameOverWindow.SetActive(false);
        FindObjectOfType<CastleHealth>().onDestroy += GameOver;
        FindObjectOfType<EnemySpawner>().onWin += WinLevel;
        StartCoroutine(HideFadePanel());
    }

    // calls at beggining of the level for smooth tranition between scenes
    IEnumerator HideFadePanel()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<FadePanel>().Hide();
        yield return new WaitForSecondsRealtime(1);
        fadePanel.SetActive(false);
    }


    // calls when move to another level/menu/restart level
    IEnumerator ShowFadePanel()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<FadePanel>().Show();
        print("begin fading");
        yield return new WaitForSecondsRealtime(1);
        print("end fading");
    }


    public void GameOver()
    {
        if (state != GameStates.NORMAL) return;

        rewardedAd.LoadAd();
        state = GameStates.GAMEOVER;
        
        gameOverWindow.SetActive(true);
    }

    public void WinLevel()
    {
        if (state != GameStates.NORMAL) return;

        rewardedAd.LoadAd();
        state = GameStates.WIN;
        winWindow.SetActive(true);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        if (state != GameStates.NORMAL) return;
        state = GameStates.PAUSE;
        pauseWindow.SetActive(true);
    }
    public void UnpauseGame()
    {
        if (state != GameStates.PAUSE) return;
        StartCoroutine(UnpauseCoroutine());
    }

    IEnumerator UnpauseCoroutine()
    {
        state = GameStates.TRANSITION;
        PauseWindow pauseComp = pauseWindow.GetComponent<PauseWindow>();
        yield return StartCoroutine(pauseComp.HidePauseWindow());
        state = GameStates.NORMAL;
    }


    public void GoNextLevel()
    {
        if (state == GameStates.TRANSITION) return;
        state = GameStates.TRANSITION;
        StartCoroutine(NextLevelCoroutine());
    }

    IEnumerator NextLevelCoroutine()
    {
        yield return StartCoroutine(ShowFadePanel());
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex + 1);
    }


    public void GoMenu()
    {
        if (state == GameStates.TRANSITION) return;
        state = GameStates.TRANSITION;
        StartCoroutine(GoMenuCoroutine());
    }

    IEnumerator GoMenuCoroutine()
    {
        yield return StartCoroutine(ShowFadePanel());
        SceneManager.LoadScene(0); // 0 - menu build index
    }

    public void BeginRestart()
    {
        if (state == GameStates.TRANSITION) return;
        state = GameStates.TRANSITION;
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {
        yield return StartCoroutine(ShowFadePanel());
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }


    public void PlayAds(int mult)
    {
        if (!isAdLoaded) return;
        adsMult = mult;
        FindObjectOfType<RewardedAd>().ShowAd();
    }

    public void AdsLoaded()
    {
        isAdLoaded = true;
        // activate buttons (probably)
    }

    public void RewardForAd()
    {
        if (state == GameStates.WIN)
           StartCoroutine(  winWindow.GetComponent<WinLevelWindow>().TripleCrystals() );
        else if (state == GameStates.GAMEOVER)
           StartCoroutine( gameOverWindow.GetComponent<GameOverWindow>().DoubleCrystals() );
    }
}

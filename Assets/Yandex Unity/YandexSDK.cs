using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK : MonoBehaviour {
    public static YandexSDK instance;
    [DllImport("__Internal")]
    private static extern void GetUserData();
    [DllImport("__Internal")]
    private static extern void ShowFullscreenAd();
    /// <summary>
    /// Returns an int value which is sent to index.html
    /// </summary>
    /// <param name="placement"></param>
    /// <returns></returns>
    [DllImport("__Internal")]
    private static extern int ShowRewardedAd(string placement);
    [DllImport("__Internal")]
    private static extern void AuthenticateUser();
    [DllImport("__Internal")]
    private static extern void InitPurchases();
    [DllImport("__Internal")]
    private static extern void Purchase(string id);

    [DllImport("__Internal")]
    private static extern void TryGetLang();

    public UserData user;
    public event Action onUserDataReceived;

    public event Action onInterstitialShown;
    public event Action<string> onInterstitialFailed;
    /// <summary>
    /// Пользователь открыл рекламу
    /// </summary>
    public event Action<int> onRewardedAdOpened;
    /// <summary>
    /// Пользователь должен получить награду за просмотр рекламы
    /// </summary>
    public event Action<string> onRewardedAdReward;
    /// <summary>
    /// Пользователь закрыл рекламу
    /// </summary>
    public event Action<int> onRewardedAdClosed;
    /// <summary>
    /// Вызов/просмотр рекламы повлёк за собой ошибку
    /// </summary>
    public event Action<string> onRewardedAdError;
    /// <summary>
    /// Getting Language
    public event Action<string> onGotLang;

    public Queue<int> rewardedAdPlacementsAsInt = new Queue<int>();
    public Queue<string> rewardedAdsPlacements = new Queue<string>();

    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
            instance.onInterstitialFailed += SDKNull;
            instance.onInterstitialShown += SDKNull;
            instance.onRewardedAdClosed += SDKNull;
            instance.onRewardedAdOpened += SDKNull;
            instance.onRewardedAdReward += SDKNull;
            instance.onRewardedAdError += SDKNull;
            StartCoroutine(ReloadRewardAd());
        }
        else {
            Destroy(gameObject);
        }
    }

    public void GetLangFromJS()
    {
        TryGetLang();
    }


    /// <summary>
    /// Call this to show rewarded ad
    /// </summary>
    /// <param name="lang"></param>
    public void GetLang(string lang)
    {
       onGotLang(lang);
    }

    void SDKNull(int i) { }
    void SDKNull(string s) { }
    void SDKNull() { }

    /// <summary>
    /// Call this to receive user data
    /// </summary>
    public void RequestUserData()
    {
        GetUserData();
    }

    /// <summary>
    /// Call this to show interstitial ad. Don't call frequently. There is a 3 minute delay after each show.
    /// </summary>
    public void ShowInterstitial() {
        if (!isRewardAdReloaded) return;
        StartCoroutine(ReloadRewardAd());
        ShowFullscreenAd();
    }

    /// <summary>
    /// Call this to show rewarded ad
    /// </summary>
    /// <param name="placement"></param>
    bool isRewardAdReloaded = true;
    public void ShowRewarded(string placement) {
        if (!isRewardAdReloaded) return;
        StartCoroutine(ReloadRewardAd());
        rewardedAdPlacementsAsInt.Enqueue(ShowRewardedAd(placement));
        rewardedAdsPlacements.Enqueue(placement);
    }

    IEnumerator ReloadRewardAd()
    {
        isRewardAdReloaded = false;
        yield return new WaitForSecondsRealtime(6);
        isRewardAdReloaded = true;
    }
    
    /// <summary>
    /// Call this to receive user data
    /// </summary>


    /// <summary>
    /// Callback from index.html
    /// </summary>
    public void OnInterstitialShown() {
        onInterstitialShown();
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="error"></param>
    public void OnInterstitialError(string error) {
        onInterstitialFailed(error);
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedOpen(int placement) {
        onRewardedAdOpened(placement);
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewarded(int placement) {
        if (placement == rewardedAdPlacementsAsInt.Dequeue()) {
            onRewardedAdReward.Invoke(rewardedAdsPlacements.Dequeue());
        }
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedClose(int placement) {
        onRewardedAdClosed(placement);
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedError(string placement) {
        onRewardedAdError(placement);
        rewardedAdsPlacements.Clear();
        rewardedAdPlacementsAsInt.Clear();
    }

}

public struct UserData {
    public string id;
    public string name;
    public string avatarUrlSmall;
    public string avatarUrlMedium;
    public string avatarUrlLarge;
}

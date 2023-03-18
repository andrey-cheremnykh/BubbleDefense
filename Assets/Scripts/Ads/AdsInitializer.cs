using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    string _androidGameId = "4667495";
    string _iOSGameId = "4667494";
    bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        print("Ads Initialized");
        int gamesPlayedCount = PlayerPrefs.GetInt("games-count");
        gamesPlayedCount++;
        PlayerPrefs.SetInt("games-count", gamesPlayedCount);
        if (gamesPlayedCount % 4 == 0)
            GetComponent<InterstitialAd>().LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
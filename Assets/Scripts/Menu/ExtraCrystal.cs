using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExtraCrystal : MonoBehaviour
{
    [SerializeField] Button watchButton;
    [SerializeField] Button closeButton;
    [SerializeField] Transform suggestText;
    void OnEnable()
    {
        Appear();
        watchButton.onClick.AddListener(ShowAd);
        closeButton.onClick.AddListener(Close);
        YandexSDK.instance.onRewardedAdReward += AddReward;
    }

    void OnDisable()
    {
        watchButton.onClick.RemoveListener(ShowAd);
        closeButton.onClick.RemoveListener(Close);
        YandexSDK.instance.onRewardedAdReward -= AddReward;
    }
    void Appear()
    {
        transform.localScale = Vector3.zero;
        suggestText.localScale = Vector3.zero;
        watchButton.transform.localScale = Vector3.zero;
        closeButton.transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f).SetEase(Ease.OutCubic).SetUpdate(true);
        suggestText.DOScale(1, 0.2f).SetDelay(0.2f).SetUpdate(true);
        watchButton.transform.DOScale(1, 0.2f).SetDelay(0.25f).SetUpdate(true);
        closeButton.transform.DOScale(1, 0.2f).SetDelay(0.25f).SetUpdate(true);
        
    } 
    void Close()
    {
        gameObject.SetActive(false);
    }
    void ShowAd()
    {
        YandexSDK.instance.ShowRewarded("extra50");
        watchButton.interactable = false;
    }
    void AddReward(string id)
    {
        if (id == "extr50")
        {
            CrystalMenuManager cm = FindObjectOfType<CrystalMenuManager>();
            cm.AddCrystals(100);
            Close();
        }
    }
}

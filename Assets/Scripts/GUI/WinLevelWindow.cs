using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class WinLevelWindow : MonoBehaviour
{
    [SerializeField] float duration = 1;
    [SerializeField] Transform[] innerAnimated;

    [SerializeField] TMP_Text crystalText;

    // Start is called before the first frame update
    void OnEnable()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("complete-level", buildIndex);
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(1, duration).SetEase(Ease.InOutBounce);
        foreach (Transform item in innerAnimated)
        {
            item.localScale = new Vector3(1, 0, 1);
            item.DOScaleY(1, duration / 2).SetEase(Ease.OutCubic).SetDelay(duration);
        }
        StartCoroutine(DisplayCrystals());
    }

    IEnumerator DisplayCrystals()
    {
        crystalText.text = "0";
        yield return new WaitForSecondsRealtime(duration * 1.1f);
        CrystalGameManager cm = FindObjectOfType<CrystalGameManager>();
        int finalCrystals = cm.SaveCrystals();
        float timer = 0;
        int crystals = 0;
        while (timer < 1)
        {
            timer += 0.01f / duration;
            crystals = (int)Mathf.Lerp(0, finalCrystals, timer);
            crystalText.text = "" + crystals;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        crystalText.text = "" + finalCrystals;
    }

    public IEnumerator TripleCrystals()
    {
        CrystalGameManager cm = FindObjectOfType<CrystalGameManager>();
        int finalCrystals = cm.SaveCrystals();
        cm.SaveCrystals();
        crystalText.text = "" + finalCrystals;

        float timer = 0;
        int crystals = 0;
        while (timer < 1)
        {
            timer += 0.01f / duration;
            crystals = (int)Mathf.Lerp(finalCrystals, finalCrystals*3, timer);
            crystalText.text = "" + crystals;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        crystalText.text = "" + finalCrystals * 3;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameOverWindow : MonoBehaviour
{

    [SerializeField] float duration = 1;
    [SerializeField] Transform[] innerAnimated;

    [SerializeField] TMP_Text crystalText;

    // Start is called before the first frame update
    public void OnEnable()
    {
        Time.timeScale = 0;
        transform.localScale = new Vector3(0,0,0);
        transform.DOScale(1, duration).SetEase(Ease.InOutBounce).SetUpdate(true);
        foreach (Transform item in innerAnimated)
        {
            item.localScale = new Vector3(1, 0, 1);
            item.DOScaleY(1, duration/2).SetEase(Ease.OutCubic).SetDelay(duration).SetUpdate(true);
        }
        StartCoroutine(DisplayCrystals());
    }

    IEnumerator DisplayCrystals()
    {
        crystalText.text = "0";
        yield return new WaitForSecondsRealtime(duration*1.1f);
        CrystalGameManager cm = FindObjectOfType<CrystalGameManager>();
        int finalCrystals = cm.SaveCrystals();
        float timer = 0;
        int crystals = 0;
        while(timer < 1)
        {
            timer += 0.01f / duration;
            crystals = (int) Mathf.Lerp(0, finalCrystals, timer);
            crystalText.text = "" + crystals;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        crystalText.text = "" + finalCrystals;
    }

    public IEnumerator DoubleCrystals()
    {
        CrystalGameManager cm = FindObjectOfType<CrystalGameManager>();
        int finalCrystals = cm.SaveCrystals();
        crystalText.text = ""+finalCrystals;
        
        float timer = 0;
        int crystals = 0;
        while (timer < 1)
        {
            timer += 0.01f / duration;
            crystals = (int)Mathf.Lerp(finalCrystals, finalCrystals*2, timer);
            crystalText.text = "" + crystals;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        crystalText.text = "" + finalCrystals * 2;
    }

    
}

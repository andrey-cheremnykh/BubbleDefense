using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class PauseWindow : MonoBehaviour
{
    [SerializeField] float duration = 1;
    [SerializeField] Transform[] innerAnimated;

    [SerializeField] Transform pauseButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0;
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(1, duration).SetEase(Ease.InOutBounce).SetUpdate(true);
        pauseButton.DOMoveX(-100, duration).SetEase(Ease.OutSine).SetUpdate(true);
        foreach (Transform item in innerAnimated)
        {
            item.localScale = new Vector3(1, 0, 1);
            item.DOScaleY(1, duration / 2).SetEase(Ease.OutCubic).SetDelay(duration).SetUpdate(true);
        }
    }

    public IEnumerator HidePauseWindow()
    {
        transform.localScale = new Vector3(1, 1, 1);
        foreach (Transform item in innerAnimated)
        {
            item.localScale = new Vector3(1, 1, 1);
            item.DOScaleY(0, duration / 2).SetEase(Ease.OutCubic).SetUpdate(true);
        }
        yield return new WaitForSecondsRealtime(duration - 0.3f);
        pauseButton.DOMoveX(100, duration).SetEase(Ease.OutSine).SetUpdate(true);
        transform.DOScale(0, duration).SetEase(Ease.InOutBounce).SetUpdate(true);
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }


   
}

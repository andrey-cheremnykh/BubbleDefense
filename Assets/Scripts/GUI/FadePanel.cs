using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadePanel : MonoBehaviour
{

    public void Show()
    {
        print("show in fade panel script");
        float duration = 1;
        Image img = GetComponent<Image>();
        print(img.enabled);
        Color color = img.color;
        //img.DOKill(true);
        img.color = new Color(color.r, color.g, color.b, 0);
        img.DOColor(new Color(color.r, color.g, color.b, 1), duration)
            .SetEase(Ease.InSine).SetUpdate(true);        
    }

    public void Hide()
    {
        float duration = 1;
        Image img = GetComponent<Image>();
        Color color = img.color;
        img.color = new Color(color.r, color.g, color.b, 1);
        img.DOColor(new Color(color.r, color.g, color.b, 0), duration)
            .SetEase(Ease.OutSine).SetUpdate(true);
    }
}

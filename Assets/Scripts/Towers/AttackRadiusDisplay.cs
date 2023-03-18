using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackRadiusDisplay : MonoBehaviour
{
    [SerializeField] float animDuration = 0.5f;

    public void EnableRadius(Vector3 origin, float radius)
    {
        transform.position = origin;
        transform.DOScale(new Vector3(radius, radius, radius), animDuration)
            .SetEase(Ease.InOutSine);
    }

    public void DisableRadius()
    {
        transform.DOScale(new Vector3(0, 0, 0), animDuration)
           .SetEase(Ease.InOutSine);
    }
}

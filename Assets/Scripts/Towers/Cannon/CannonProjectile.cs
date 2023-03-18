using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonProjectile : MonoBehaviour
{
    [SerializeField] int levelIndex = 0;

    bool isLaunched = false;
    float flightDuration = 0.8f;

    Vector3 prevPos;
    CannonPoolChoser cannonPool;

    private void Start()
    {
        cannonPool = FindObjectOfType<CannonPoolChoser>();
    }

    public IEnumerator Launch(Vector3 destination)
    {
        isLaunched = true;
        prevPos = transform.position;
        transform.DOMoveX(destination.x, flightDuration).SetEase(Ease.Linear);
        transform.DOMoveZ(destination.z, flightDuration).SetEase(Ease.Linear);
        transform.DOMoveY(destination.y + 12, flightDuration / 2)
            .SetEase(Ease.OutSine).SetLoops(2, LoopType.Yoyo);

        yield return new WaitForSeconds(flightDuration);
        // ToDO For the next
        PlayBurstVFX();
        DamageEnemyInRadius();
        isLaunched = false;
        cannonPool.ReturnCannon(gameObject, levelIndex);
    }

    void PlayBurstVFX()
    {
        cannonPool.PlayBurstEffect(levelIndex, transform.position);
    }

    void DamageEnemyInRadius()
    {
        // optimization
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        float radius = GameConstants.RADIUS_EXPLOSION_CANNON[levelIndex];
        SetupUpgradesInLevel setup = FindObjectOfType<SetupUpgradesInLevel>();
        float muliplyer = 1 + setup.TowerDamagePercentage;
        float damage = GameConstants.DAMAGE_FOR_CANNON[levelIndex] * muliplyer;
        foreach (EnemyHealth en in enemies)
        {
            float distance = Vector3.Distance(transform.position, en.transform.position);
            if (distance < radius)
                en.GetDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaunched == false) return;
        Vector3 curPos = transform.position;
        Quaternion rotateTo = Quaternion.LookRotation(curPos-prevPos);
        transform.rotation = rotateTo;
        prevPos = curPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float radius = GameConstants.RADIUS_EXPLOSION_CANNON[levelIndex];
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}

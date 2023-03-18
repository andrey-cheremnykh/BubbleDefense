using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    ArrowPool arrowPool;

    float fireRate = 2;
    float damage = 10;

    [SerializeField] Arrow arrowLoaded;
    Vector3 arrowSpawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        arrowPool = FindObjectOfType<ArrowPool>();
        arrowSpawnPosition = arrowLoaded.transform.localPosition;
    }


    public void Shoot(Transform enemy, float damage)
    {
        this.damage = damage;
        if (arrowLoaded == null) return;
        arrowLoaded.Launch(enemy, damage);
        arrowLoaded = null;
        StartCoroutine(ReloadCrossbow());
    }

    IEnumerator ReloadCrossbow()
    {
        yield return new WaitForSeconds(1/fireRate);
        GameObject cloneArrow = arrowPool.GetObject();
        cloneArrow.transform.parent = transform;
        cloneArrow.transform.localPosition = arrowSpawnPosition;
        cloneArrow.transform.localRotation = Quaternion.Euler(0, 180, 0);
        arrowLoaded = cloneArrow.GetComponent<Arrow>();
    }

   
}

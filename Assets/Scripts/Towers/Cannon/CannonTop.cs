using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonTop : MonoBehaviour
{
    [SerializeField] Mesh[] topMeshes;
    [SerializeField] float[] heights;

    CannonPoolChoser cannonPool;

    float fireRate = 1;

    MeshFilter formMesh;
    bool isReloaded = true;
    int level = 0;

    public void ChangeLevel(int levelIndex)
    {
        formMesh = GetComponent<MeshFilter>();
        formMesh.mesh = topMeshes[levelIndex];
        transform.localPosition = new Vector3(0, heights[levelIndex] ,0);
        isReloaded = true;
        level = levelIndex;
    }

    public IEnumerator Shoot(Vector3 enemyPos)
    {
        if (!isReloaded) yield break;

        float downPosY = transform.position.y - 0.7f;
        transform.DOMoveY(downPosY, 0.2f).SetEase(Ease.InCubic).SetLoops(2, LoopType.Yoyo);
        SpawnCannon(enemyPos);

        isReloaded = false;
        yield return new WaitForSeconds(1/fireRate);
        isReloaded = true;

    }

    void SpawnCannon(Vector3 dest)
    {
        Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
        GameObject newCannon = cannonPool.GetCannon(level);
        newCannon.transform.position = spawnPos;
        CannonProjectile c = newCannon.GetComponent<CannonProjectile>();

        StartCoroutine(c.Launch(dest));
    }

    // Start is called before the first frame update
    void Start()
    {
        cannonPool = FindObjectOfType<CannonPoolChoser>();
    }
    
}

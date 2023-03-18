using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicCrystal : MonoBehaviour
{
    [SerializeField] Mesh[] levelMeshes;
    [SerializeField] float[] heights;

    [SerializeField] float rateMoving = 4;
    [SerializeField] float rotateSpeed = 360;

    LineRenderer[] lines; // lvl 1-3: index = 0; lvl 4a: index = 1; lvl 4b: index = 2;

    LineRenderer currentLine;

    MeshFilter meshFilter;

    float damage;
    float slowness;

    void Start()
    {
        lines = GetComponentsInChildren<LineRenderer>();
        currentLine = lines[0];
    }

    public void ChangeCrystal(int levelIndex)
    {
        meshFilter = GetComponent<MeshFilter>();
        levelIndex--;
        meshFilter.mesh = levelMeshes[levelIndex];
        transform.localPosition = new Vector3(0, heights[levelIndex], 0);
        SetupUpgradesInLevel setup = FindObjectOfType<SetupUpgradesInLevel>();
        float muliplyer = 1 + setup.TowerDamagePercentage;
        damage = GameConstants.DAMAGE_FOR_MAGIC[levelIndex]* muliplyer;
        slowness = (float)GameConstants.SLOWNESS_FOR_MAGIC[levelIndex]/100;
        transform.DOLocalMoveY(heights[levelIndex] + 1.5f, 1 / rateMoving)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        transform.DORestart();

        if (levelIndex == 3) currentLine = lines[1];
        else if (levelIndex == 4) currentLine = lines[2];
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }

    public void Shoot(EnemyHealth enemy)
    {
        enemy.GetDamage(damage);
        EnemyAction enMovement = enemy.GetComponent<EnemyAction>();
        StartCoroutine(enMovement.SlowEnemyDown(slowness, 1));
        StartCoroutine(DisplayLine(enemy.transform.position));
        // display particle effects around enemy (probably)
    }

    IEnumerator DisplayLine(Vector3 enemyPos)
    {
        currentLine.SetPosition(0, transform.position);
        currentLine.SetPosition(1, enemyPos + new Vector3(0, 0.5f, 0));

        yield return new WaitForSeconds(0.05f);

        currentLine.SetPosition(0, new Vector3());
        currentLine.SetPosition(1, new Vector3());
    }

    public void SplashShoot(EnemyHealth[] enemies)
    {
        foreach (EnemyHealth enemy in enemies)
        {
            enemy.GetDamage(damage);
            EnemyAction enMovement = enemy.GetComponent<EnemyAction>();
            StartCoroutine(enMovement.SlowEnemyDown(slowness, 1));
        }
        StartCoroutine(DisplayLineSplash(enemies));

    }

    IEnumerator DisplayLineSplash(EnemyHealth[] enemies)
    {
        int pointCount = enemies.Length * 2;
        currentLine.positionCount = pointCount;
        for (int i = 0; i < pointCount; i = i + 2)
        {
            Vector3 enPos = enemies[i / 2].transform.position + new Vector3(0, 0.5f, 0);
            Vector3 originPos = transform.position;
            currentLine.SetPosition(i, enPos);
            currentLine.SetPosition(i+1, originPos);
        }
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < pointCount; i++)
        {
            currentLine.SetPosition(i, Vector3.zero);
        }

    }

}

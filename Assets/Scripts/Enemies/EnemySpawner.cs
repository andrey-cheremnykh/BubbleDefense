using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    Pathfinder pathfinder;
    [SerializeField] Button spawnWaveButton;

    [SerializeField] Wave[] waves;
    [SerializeField] float waveGap = 30;

    int waveCount = 0;
    public bool IsSpawning { get; private set; }

    Coroutine lastCountCoroutine;

    public event Action onWin;

    // Start is called before the first frame update
    void Start()
    {
        IsSpawning = false;
        pathfinder = GetComponent<Pathfinder>();
        spawnWaveButton.onClick.AddListener(SpawnNewWave);
    }

    void SpawnNewWave()
    {
        if (IsSpawning == true) return;
        StartCoroutine(SpawnSingleWave());
    }

    IEnumerator SpawnSingleWave()
    {
        if(lastCountCoroutine != null) StopCoroutine(lastCountCoroutine);
        IsSpawning = true;
        List<Vector3> path = pathfinder.FindPath();
        spawnWaveButton.GetComponent<SpawnButton>().DisplayNewWave(waveCount);
        FindObjectOfType<BuildTowerManager>().DeselectWaypoint();
        yield return StartCoroutine(waves[waveCount].SpawnWaveEnemies(path));
        waveCount++;
        lastCountCoroutine = StartCoroutine(CheckEnemiesDead());
    }

    IEnumerator CheckEnemiesDead()
    {
        yield return new WaitForSeconds(0.5f);
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        if(enemies.Length == 0)
        {
            if(waveCount == waves.Length)
            {
                onWin?.Invoke();
                yield break;
            }
            IsSpawning = false;
            SpawnButton spawnButton = spawnWaveButton.GetComponent<SpawnButton>();
            StartCoroutine(spawnButton.DecreaseBar(waveGap));
            
            yield return new WaitForSeconds(waveGap);
            SpawnNewWave();
        }
        else
        {
            lastCountCoroutine = StartCoroutine(CheckEnemiesDead());
        }
    }

    
}

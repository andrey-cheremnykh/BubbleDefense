using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Wave : ScriptableObject
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float durationBetween = 2;
    [SerializeField] int enemyCount = 6;
    [SerializeField] float healthMult = 1;
    int enemyIndex = 0;

    public IEnumerator SpawnWaveEnemies(List<Vector3> path)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject clone = Instantiate(ChooseEnemyPrefab(), path[0], Quaternion.identity);
            clone.GetComponent<EnemyAction>().StartTheWay(path);
            clone.GetComponent<EnemyHealth>().IncreaseHealth(healthMult);
            yield return new WaitForSeconds(durationBetween);
        }
    }
    GameObject ChooseEnemyPrefab()
    {
        int prefabCount = enemyPrefabs.Length;
        GameObject enemyPrefab = enemyPrefabs[enemyIndex];
        enemyIndex++;
        if (enemyIndex >= prefabCount) enemyIndex = 0;
        return enemyPrefab;
    }


}

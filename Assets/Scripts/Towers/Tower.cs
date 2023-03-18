using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TowerState
{
    BUILDING,
    DESTROYING,
    LEVEL_1,
    LEVEL_2,
    LEVEL_3,
    LEVEL_4A,
    LEVEL_4B
}

public class Tower : MonoBehaviour
{
    [HideInInspector] public TowerState tState = TowerState.BUILDING;

    public float AttackRadius { protected set; get; }

    [SerializeField] Mesh[] levelMeshes_1;
    [SerializeField] Mesh[] levelMeshes_2;
    [SerializeField] Mesh[] levelMeshes_3;
    [SerializeField] Mesh[] levelMeshes_4A;
    [SerializeField] Mesh[] levelMeshes_4B;

    MeshFilter towerMesh;
    ParticleSystem buildVFX;
    Waypoint waypointOn;

    public void SetWaypointOn(Waypoint w)
    {
        waypointOn = w;
    }

    void Start()
    {
        towerMesh = GetComponent<MeshFilter>();
        buildVFX = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(BuildTheTower());
    }

    protected virtual IEnumerator BuildTheTower()
    {
        towerMesh.mesh = levelMeshes_1[0];
        buildVFX.Play();
        yield return new WaitForSeconds(5);
        towerMesh.mesh = levelMeshes_1[1];
        buildVFX.Stop();
        tState = TowerState.LEVEL_1;
    }

    public virtual IEnumerator UpgradeToLevel_2()
    {
        buildVFX.Play();
        towerMesh.mesh = levelMeshes_2[0];
        tState = TowerState.BUILDING;
        yield return new WaitForSeconds(5);
        towerMesh.mesh = levelMeshes_2[1];
        tState = TowerState.LEVEL_2;
        buildVFX.Stop();
    }

    public virtual IEnumerator UpgradeToLevel_3()
    {
        buildVFX.Play();
        towerMesh.mesh = levelMeshes_3[0];
        tState = TowerState.BUILDING;
        yield return new WaitForSeconds(5);
        towerMesh.mesh = levelMeshes_3[1];
        tState = TowerState.LEVEL_3;
        buildVFX.Stop();
    }

    public virtual IEnumerator UpgradeToLevel_4A()
    {
        buildVFX.Play();
        towerMesh.mesh = levelMeshes_4A[0];
        tState = TowerState.BUILDING;
        yield return new WaitForSeconds(5);
        towerMesh.mesh = levelMeshes_4A[1];
        tState = TowerState.LEVEL_4A;
        buildVFX.Stop();
    }


    public virtual IEnumerator UpgradeToLevel_4B()
    {
        buildVFX.Play();
        towerMesh.mesh = levelMeshes_4B[0];
        tState = TowerState.BUILDING;
        yield return new WaitForSeconds(5);
        towerMesh.mesh = levelMeshes_4B[1];
        tState = TowerState.LEVEL_4B;
        buildVFX.Stop();
    }

    public virtual IEnumerator DestroyTower()
    {
        buildVFX.Play();
        if (tState == TowerState.LEVEL_1) towerMesh.mesh = levelMeshes_1[0];
        if (tState == TowerState.LEVEL_2) towerMesh.mesh = levelMeshes_2[0];
        if (tState == TowerState.LEVEL_3) towerMesh.mesh = levelMeshes_3[0];
        if (tState == TowerState.LEVEL_4A) towerMesh.mesh = levelMeshes_4A[0];
        if (tState == TowerState.LEVEL_4B) towerMesh.mesh = levelMeshes_4B[0];
        tState = TowerState.DESTROYING;
        yield return new WaitForSeconds(5);
        waypointOn.towerOnPoint = null;
        buildVFX.Stop();
        Destroy(gameObject);
    }




    protected List<EnemyAction> FindAllEnemiesInRadius()
    {
        List<EnemyAction> enemiesInRadius = new List<EnemyAction>();
        EnemyAction[] enemies = FindObjectsOfType<EnemyAction>();
        foreach (EnemyAction en in enemies)
        {
            if (en.GetComponent<EnemyHealth>().IsAlive == false) continue;
            float distance = Vector3.Distance(transform.position, en.transform.position);
            if (distance < AttackRadius)
                enemiesInRadius.Add(en);
        }

        return enemiesInRadius;
    }

    protected EnemyAction FindEnemyToShoot()// find the closest enemy to end point 
    { 
        List<EnemyAction> enemies = FindAllEnemiesInRadius();
        if (enemies.Count == 0) return null;

        EnemyAction enemyToShoot = enemies[0];
        for (int i = 1; i < enemies.Count; i++)
        {
            if (enemyToShoot.GetPathPassed() < enemies[i].GetPathPassed())
                enemyToShoot = enemies[i];
        }

        return enemyToShoot;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

   /* private void OnMouseUpAsButton()
    {
        if (tState == TowerState.BUILDING || tState == TowerState.DESTROYING) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        UpgradeTowerManager utm = FindObjectOfType<UpgradeTowerManager>();
        utm.SelectTower(this);
    }
*/

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    float enemyOriginSpeed;
    [SerializeField] float enemySpeed = 1; // How many tiles per second your enemy moves
    int tilesPassed = 0;
    float timer = 0f;
    [SerializeField] Material baseMaterial;
    [SerializeField] Material frozenMaterial;
    SkinnedMeshRenderer meshRenderer;
    Vector3 extraOffset;

    bool isMoving = true;

    public float GetPathPassed()
    {
        return tilesPassed + timer;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyOriginSpeed = enemySpeed;
        GetComponent<EnemyHealth>().onDeath += StopMoving;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        SetRandomOffset();
    }

    void SetRandomOffset()
    {
        float x = Random.Range(-2.5f, 2.5f);
        float y = Random.Range(-1f, 0f);
        float z = Random.Range(-2.5f, 2.5f);
        extraOffset = new Vector3(x,y,z);
    }

    void StopMoving()
    {
        isMoving = false;
    }
    
    public void StartTheWay(List<Vector3> path)
    {
        StartCoroutine(MoveByPath(path));
    }

    public IEnumerator MoveByPath(List<Vector3> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            RotateProperly(path[i], path[i + 1]);
            yield return StartCoroutine(MoveBetweenPoints(path[i], path[i+1]));
            tilesPassed++;
        }
        StartCoroutine(GoToCastle(path));
    }


    IEnumerator GoToCastle(List<Vector3> path)
    {
        AttackPointChooser pointChooser = FindObjectOfType<AttackPointChooser>();
        AttackPoint freePoint = pointChooser.GetFreePoint();
        if (freePoint)
        {
            freePoint.enemy = gameObject;
            Vector3 pos = freePoint.transform.position + Vector3.down * 10;
            Vector3 startPos = path[path.Count - 1];
            yield return StartCoroutine(MoveBetweenPoints(startPos, pos - extraOffset));
            GetComponent<Animator>().SetTrigger("attack");
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(GoToCastle(path));
        }
    }
  

    void RotateProperly(Vector3 start, Vector3 end)
    {
        if (isMoving == false) return;
        Vector3 dir = end - start;
        Vector3 lookPoint = transform.position + dir;
        transform.LookAt(lookPoint);
    }

    IEnumerator MoveBetweenPoints(Vector3 posStart, Vector3 posEnd)
    {
        timer = 0;
 
        while(timer < 1)
        {
            if (isMoving == false) yield break;
            timer += Time.deltaTime * enemySpeed;
            Vector3 pos = Vector3.Lerp(posStart, posEnd, timer) + extraOffset;
            transform.position = pos + Vector3.up * 10;
            yield return null;
        }

        transform.position = posEnd + Vector3.up * 10 + extraOffset;
    }

    public virtual IEnumerator SlowEnemyDown(float slowness, float duration)
    {
        meshRenderer.material = frozenMaterial;
        enemySpeed = enemySpeed * (1 - slowness);
        yield return new WaitForSeconds(duration);
        enemySpeed = enemySpeed / (1 - slowness);

        if(enemySpeed >= 0.99f * enemyOriginSpeed)
            meshRenderer.material = baseMaterial;
    }

}

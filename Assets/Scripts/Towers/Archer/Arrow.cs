using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Vector3 startPos;
    Transform enemy;

    float timer = 0;
    float timeToEnemy = 0.3f;

    float damage = 10;
    bool isLaunched = false;

    [SerializeField] AudioClip shootSound;

    public void Launch(Transform enemy, float damage)
    {
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
        this.enemy = enemy;
        startPos = transform.position;
        this.damage = damage;
        isLaunched = true;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaunched) return;
        timer += Time.deltaTime / timeToEnemy;
        Vector3 endPos = enemy.position + new Vector3(0, 0.5f, 0);
        transform.position = Vector3.Lerp(startPos, endPos, timer);
        transform.LookAt(enemy.position);

        if(timer >= 1)
        {
            ReachTheEnemy();
        }

    }

    void ReachTheEnemy()
    {
        timer = 0;
        isLaunched = false;
        enemy.GetComponent<EnemyHealth>().GetDamage(damage);
        FindObjectOfType<ArrowPool>().ReturnObject(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    SkinnedMeshRenderer meshRenderer;
    [SerializeField] Material dissolveMaterial;
    [SerializeField] Material baseMaterial;


    public bool IsAlive { private set; get; }

    public void IncreaseHealth(float percentage)
    {
        health = health * percentage;
    }

    public event Action onDeath;

    void Start()
    {
        IsAlive = true;
        onDeath += Death;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        StartCoroutine(AppearEnemy());
    }

    IEnumerator AppearEnemy()
    {
        meshRenderer.material = dissolveMaterial;
        float start = 0.8f;
        float end = -1;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 2;
            float value = Mathf.Lerp(start, end, timer);
            meshRenderer.material.SetFloat("_transparency", value);
            yield return null;
        }
        meshRenderer.material = baseMaterial;
    }

    public void GetDamage(float damage)
    {
        if (!IsAlive) return;

        health -= damage;
        if(health <= Mathf.Epsilon)
        {
            onDeath?.Invoke();
        }
    }

    void Death()
    {
        GetComponent<Animator>().SetTrigger("die");
        IsAlive = false;
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(2);
        meshRenderer.material = dissolveMaterial;
        float start = -1;
        float end = 0.8f;
        float timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime;
            float value = Mathf.Lerp(start, end, timer);
            meshRenderer.material.SetFloat("_transparency", value);
            yield return null;
        }
        Destroy(gameObject);
    }

}

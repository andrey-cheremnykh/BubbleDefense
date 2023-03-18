using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] int startCapacity = 5;
    [SerializeField] GameObject prefab;
    Queue<GameObject> pool;

    // Start is called before the first frame update
    void Awake()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < startCapacity; i++)
        {
            GameObject clone = Instantiate(prefab, transform);
            clone.SetActive(false);
            pool.Enqueue(clone);
        }
    }
    
    public GameObject GetObject()
    {
        if(pool.Count > 0)
        {
            GameObject refreshedObj = pool.Dequeue();
            refreshedObj.SetActive(true);
            return refreshedObj;
        }
        else
        {
            GameObject clone = Instantiate(prefab, transform);
            return clone;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(0, 0, 0);
        pool.Enqueue(obj);
    }

    
}

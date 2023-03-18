using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPoolChoser : MonoBehaviour
{
    CannonPool[] pools;
    [SerializeField] ParticleSystem[] burstVFX;

    // Start is called before the first frame update
    void Start()
    {
        pools = GetComponentsInChildren<CannonPool>();
    }

    public GameObject GetCannon(int index)
    {
        return pools[index].GetObject();
    }

    public void ReturnCannon(GameObject retObj,int index)
    {
        pools[index].ReturnObject(retObj);
    }

    public void PlayBurstEffect(int projLevel, Vector3 playPos)
    {
        burstVFX[projLevel].transform.position = playPos;
        burstVFX[projLevel].Play();
    }
    
}

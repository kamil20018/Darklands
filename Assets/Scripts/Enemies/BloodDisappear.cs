using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDisappear : MonoBehaviour
{
    public float onScreenTime = 3f;
    float spawnedAt;
    void Start()
    {
        spawnedAt = Time.time;
    }
    private void Update()
    {
        if(Time.time - spawnedAt > onScreenTime)
        {
            Destroy(gameObject);
        }
    }
}

﻿using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Chunk chunk;
    void Start()
    {
        for(int x = 0; x < 0; x++) //disabling mouse spawning for testing
        {
            AddMouse();
        }

    }

    public void AddMouse()
    {
        GameObject mouse;
        mouse = Resources.Load<GameObject>("Prefabs/Mouse");
        int x = Random.Range(-10, 10);
        int y = Random.Range(-10, 10);
        Vector3 spot = new Vector3(x, y, 0);
        mouse = Instantiate(mouse, transform.position + spot, Quaternion.identity);
        mouse.transform.parent = gameObject.transform;
        //mouse.transform.position += new Vector3(0, 0, -1);
        mouse.GetComponent<EnemyHealthManager>().SetStats(PlayerStats.level, 30, 120, 10);
    }
}

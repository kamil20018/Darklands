using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            aoe();
        }   
    }

    void aoe()
    {
        GameObject circle = Resources.Load<GameObject>("Prefabs/CircleAttack");
        Instantiate(circle, Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, -1), Quaternion.identity);
    }
}

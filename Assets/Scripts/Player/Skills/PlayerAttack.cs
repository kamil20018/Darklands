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
            Aoe();
        }   
        if (Input.GetKeyDown("2"))
        {
            AcidPool();
        }
    }

    private void Aoe()
    {
        GameObject circle = Resources.Load<GameObject>("Prefabs/CircleAttack");
        Vector3 properPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        properPos.z = 0;
        Instantiate(circle, properPos, Quaternion.identity);
    }

    private void AcidPool()
    {
        GameObject acid = Resources.Load<GameObject>("Prefabs/AcidPool");
        Vector3 properPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        properPos.z = 0;
        Instantiate(acid, properPos, Quaternion.identity);
    }
}

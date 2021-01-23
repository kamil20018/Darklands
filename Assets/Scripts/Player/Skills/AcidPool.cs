using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private int dpt;
    private float duration = 10f;
    private float started;
    private float tick = 0.1f;
    private float lastHit = 0f;
    // Start is called before the first frame update
    void Start()
    {
        started = Time.deltaTime;
        dpt = 20;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

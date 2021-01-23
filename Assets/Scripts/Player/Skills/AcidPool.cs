using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private int dpt;
    private float duration = 5f;
    private float started;
    private float tick = 0.1f;
    private float lastHit;
    // Start is called before the first frame update
    void Start()
    {
        started = Time.time;
        lastHit = Time.time;
        dpt = 5;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && lastHit + tick < Time.time)
        {
            lastHit = Time.time;
            collision.GetComponent<EnemyHealthManager>().TakeDamage(dpt);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(started + duration < Time.time){
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    private int baseDamage = 100, damage;

    void Start()
    {
        damage = PlayerStats.level * baseDamage;
        StartCoroutine(end());
    }

    private IEnumerator end()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().TakeDamage(damage);
        }
    }
}

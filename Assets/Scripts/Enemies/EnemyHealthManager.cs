using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    private int level = 1;
    [SerializeField]
    private float maxHp, hp;
    private int xpOnDeath;

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if(hp <= 0)
        {
            PlayerStats.currentXp += xpOnDeath;
            GameObject bloodPuddle = Resources.Load<GameObject>("Prefabs/BloodPuddle");
            bloodPuddle = Instantiate(bloodPuddle, transform.position, Quaternion.identity);
            bloodPuddle.transform.parent = gameObject.transform.parent;
            bloodPuddle.transform.position += new Vector3(0, 0, -2);
            Destroy(gameObject);
        }
    }

    public void SetStats(int setLevel, int basicXp, float setBasicMaxHp, float basicDamage)
    {
        level = setLevel;
        xpOnDeath = basicXp * level;
        maxHp = level * setBasicMaxHp;
        hp = maxHp;
    }
}

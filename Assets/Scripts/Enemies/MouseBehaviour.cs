using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    public Rigidbody2D playerBody;
    private Rigidbody2D mouseBody;

    void Start()
    {
        GameObject test = GameObject.Find("PlayerUgly");
        playerBody = test.GetComponent<Rigidbody2D>();
        mouseBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 toPlayer = playerBody.transform.position - mouseBody.transform.position;
        Vector2 toPlayer2 = new Vector2(toPlayer.x, toPlayer.y);

        if (toPlayer2.magnitude > Chunk.fromCenter)
        {
            mouseBody.velocity = Vector2.zero;
        }
        else
        {
            mouseBody.velocity = toPlayer2.normalized;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //GameObject bloodPuddle = Resources.Load<GameObject>("Prefabs/BloodPuddle");
        //Instantiate(bloodPuddle, transform.position, Quaternion.identity);
        //bloodPuddle.transform.parent = gameObject.transform.parent;
    }

}

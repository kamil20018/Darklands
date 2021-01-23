using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public Rigidbody2D playerBody;
    private Vector2 move;
    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() //like update, but on a fixed timer, so its more reliable for physics
    {
        playerBody.MovePosition(playerBody.position + move.normalized * movementSpeed * Time.deltaTime);
    }
}

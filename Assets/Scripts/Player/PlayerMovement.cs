using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Speed speed;
    public Rigidbody2D rb;
  
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        float x = horizontal * Time.deltaTime * speed.Value;
        float y = vertical * Time.deltaTime * speed.Value;

        rb.MovePosition(rb.position + new Vector2(x, y));
    }
}

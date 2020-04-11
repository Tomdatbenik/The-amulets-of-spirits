using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Speed speed;
    public StatHolder statHolder;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement = new Vector2(0, 0);

    private void Start()
    {
        speed = statHolder.FindPropertyByName("Speed") as Speed;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

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

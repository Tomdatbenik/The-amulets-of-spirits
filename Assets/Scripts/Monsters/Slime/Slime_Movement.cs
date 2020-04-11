using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Movement : MonoBehaviour
{
    public Speed speed;
    public GameObject Player;
    private Vector2 movement;
    public Rigidbody2D rb;

    public bool Attacking;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CalculateMovement();
    }

    private void FixedUpdate()
    {
        if(!Attacking)
        {
            Move();
        }
    }

    private void Move()
    {
        rb.MovePosition((Vector2)rb.transform.position + (movement * speed.Value * Time.deltaTime));
    }

    private void CalculateMovement()
    {
        if(Player != null)
        {
            Vector2 Direction = Player.transform.position - transform.position;

            Direction.Normalize();
            movement = Direction;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarMovement : MonoBehaviour
{
    public BoarTrigger LeftAttackTrigger;
    public BoarTrigger RightAttackTrigger;
    public BoarTrigger Vision;

    Vector2 Direction;

    bool Charging;
    bool Attacking;

    GameObject player;

    Vector2 Target;

    public Rigidbody2D rb;

    public StatHolder statholder;
    private Speed speed;

    public Animator animator;

    public GameObject hitbox;

    private bool cooldown;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Target = player.transform.position;

        speed = statholder.FindPropertyByName("Speed") as Speed;
    }

    private void Update()
    {
        Target = player.transform.position;
        CalculateMovement();
    }

    private void FixedUpdate()
    {
        if(LeftAttackTrigger.PlayerInTrigger || RightAttackTrigger.PlayerInTrigger)
        {
            if(!Charging && !animator.GetBool("Attacking") && !cooldown)
            {
                cooldown = true;
                StartCoroutine(Cooldown());
                StartCoroutine(ChargeUpAttack());
                Charging = true;
            }
        }


        if (Vision.PlayerInTrigger && !Charging && !Attacking)
        {
            Move(); 
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    public void Move()
    {
        rb.MovePosition((Vector2)rb.transform.position + (Direction * speed.Value * Time.deltaTime));
    }


    private void CalculateMovement()
    {
        if (Target != null)
        {
            Direction = Target - new Vector2(rb.transform.position.x, rb.transform.position.y);

            Direction.Normalize();
            animator.SetFloat("Horizontal", Direction.x);
        }
    }

    public IEnumerator ChargeUpAttack()
    {
        animator.SetBool("Charging", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Charging", false);
        animator.SetBool("Attacking", true);
        StartCoroutine(Attack());
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);
        cooldown = false;
    }

    public IEnumerator Attack()
    {
        hitbox.SetActive(true);
        yield return new WaitForSeconds(.5f);
        Charging = false;
        hitbox.SetActive(false);
        animator.SetBool("Attacking", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandDamgeTrigger : MonoBehaviour
{
    public float timeBetweenDamageTicks;
    public AudioSource audioSource;
    public StatHolder statholder;

    public bool canDamagePlayer = true;
    float endTime;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (canDamagePlayer)
            {
                endTime = Time.time + timeBetweenDamageTicks;
                canDamagePlayer = false;

                StatHolder enemyStatholder = collision.GetComponent<StatHolder>();

                ApplyDamage(enemyStatholder);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (canDamagePlayer)
            {
                endTime = Time.time + timeBetweenDamageTicks;
                canDamagePlayer = false;

                StatHolder enemyStatholder = collision.GetComponent<StatHolder>();

                ApplyDamage(enemyStatholder);
            }
        }
    }

    private void Update()
    {
        if(!canDamagePlayer)
        {
            if (Time.time > endTime)
            {
                canDamagePlayer = true;
            }
        }
    }

    private void ApplyDamage(StatHolder enemyStatHolder)
    {
        Health health = enemyStatHolder.FindPropertyByName("Health") as Health;
        enemyStatHolder.DisplayDamage(enemyStatHolder.gameObject.GetComponent<SpriteRenderer>());
        if (health != null)
        {
            if (health.Hurt != null)
            {
                audioSource.clip = health.Hurt;
                audioSource.Play();
            }

            health.ApplyDamage((Attack)statholder.FindPropertyByName("Attack"));
        }
    }
}

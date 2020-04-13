using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxApplyAttack : MonoBehaviour
{
    public StatHolder statHolder;
    public List<string> tags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tags.Contains(collision.tag))
        {
            StatHolder enemyStatholder = collision.GetComponent<StatHolder>();

            ApplyDamage(enemyStatholder);
        }
    }

   

    private void ApplyDamage(StatHolder enemyStatHolder)
    {
        Health health = enemyStatHolder.FindPropertyByName("Health") as Health;
        enemyStatHolder.DisplayDamage(enemyStatHolder.gameObject.GetComponent<SpriteRenderer>());
        if (health != null)
        {
            health.ApplyDamage((Attack)statHolder.FindPropertyByName("Attack"));
        }
    }
}

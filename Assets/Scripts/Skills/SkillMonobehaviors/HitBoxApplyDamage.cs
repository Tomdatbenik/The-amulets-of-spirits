using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxApplyDamage : MonoBehaviour
{
    public Attack attack;
    public List<string> tags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(tags.Contains(collision.tag))
        {
            StatHolder statHolder = collision.GetComponent<StatHolder>();
            statHolder.DisplayDamage(collision.GetComponent<SpriteRenderer>());
            Health health = statHolder.FindPropertyByName("Health") as Health;

            if (health != null)
            {
                health.ApplyDamage(attack);
            }
        }
    }
}

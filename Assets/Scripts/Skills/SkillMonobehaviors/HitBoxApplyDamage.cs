using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxApplyDamage : MonoBehaviour
{
    public Damage damage;
    public List<string> tags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(tags.Contains(collision.tag))
        {
            StatHolder statHolder = collision.GetComponent<StatHolder>();

            Health health = statHolder.properties.Find(p => p.GetType().Name == "Health") as Health;

            if(health != null)
            {
                health.ApplyDamage(damage);
            }
        }
    }
}

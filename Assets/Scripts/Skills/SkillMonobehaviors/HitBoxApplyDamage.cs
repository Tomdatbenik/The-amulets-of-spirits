using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxApplyDamage : MonoBehaviour
{
    public Attack attack;
    public List<string> tags;
    public AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(tags.Contains(collision.tag))
        {
            StatHolder statHolder = collision.GetComponent<StatHolder>();
            statHolder.DisplayDamage(collision.GetComponent<SpriteRenderer>());
            Health health = statHolder.FindPropertyByName("Health") as Health;

            if (health != null)
            {
                if (health.Hurt != null)
                {
                    audioSource.clip = health.Hurt;
                    audioSource.Play();
                }

                health.ApplyDamage(attack);
            }
        }
    }
}

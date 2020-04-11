using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack : MonoBehaviour
{
    public Slime_Movement movement;
    public GameObject Hitbox;

    public float chargeUp;
    public float HitboxLastFor;

    public ParticleSystem Charge;
    public ParticleSystem splashDamage;
    private bool attacking;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            movement.Attacking = true;
            if (!attacking)
            {
                attacking = true;
                StartCoroutine(ChargeUp());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            movement.Attacking = true;
            if(!attacking)
            {
                attacking = true;
                StartCoroutine(ChargeUp());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Hitbox.SetActive(false);
        }
    }

    public IEnumerator ChargeUp()
    {
        Charge.Play();
        yield return new WaitForSeconds(chargeUp);
        StartCoroutine(SetHitBoxActiveForTime());
    }


    public IEnumerator SetHitBoxActiveForTime()
    {
        splashDamage.Play();
        Hitbox.SetActive(true);
        yield return new WaitForSeconds(HitboxLastFor);
        Hitbox.SetActive(false);
        attacking = false;
        movement.Attacking = false;
    }
}

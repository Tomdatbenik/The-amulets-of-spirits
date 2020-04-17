using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireAttack : MonoBehaviour
{
    public GameObject ProjectileSpawner;
    public float speed;

    public Vector2 startMarker;
    public Vector2 endMarker;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public float timeBetweenDamageTicks;
    public AudioSource audioSource;
    public Attack attack;

    public bool canDamagePlayer = true;
    float endTime;

    public float LifeTimeInSeconds;

    private void Awake()
    {
        startMarker = new Vector2(ProjectileSpawner.transform.position.x, ProjectileSpawner.transform.position.y);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        endMarker = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    private void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker, endMarker);

        //transform.right = wielder.transform.position - transform.position;
        audioSource = GameObject.FindGameObjectWithTag("BossControllerObject").GetComponent<AudioSource>();
        StartCoroutine(DissapearAfterTime());
    }

    private void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector2.Lerp(startMarker, endMarker, fractionOfJourney);

        if (!canDamagePlayer)
        {
            if (Time.time > endTime)
            {
                canDamagePlayer = true;
            }
        }
    }



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

            health.ApplyDamage(attack);
        }
    }

    private IEnumerator DissapearAfterTime()
    {
        yield return new WaitForSeconds(LifeTimeInSeconds);
        Destroy(gameObject);
    }
}

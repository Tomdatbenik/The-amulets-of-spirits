using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private StatHolder statholder;

    public List<BossTrigger> triggers;

    public GameObject Hands;

    public GameObject LeftHand;
    public GameObject RightHand;
    public ParticleSystem LeftHandParticles;
    public ParticleSystem RightHandParticles;

    public GameObject Body;

    private Animator bodyAnimator;
    private Animator handsAnimator;

    public GameObject ProjectileLauncher;
    public GameObject BossFire;

    private List<BossAttack> AvailebleAttacks = new List<BossAttack>();

    public int SecondsBetweenAttack;

    bool canAttack = true;

    bool punching = false;

    Health bosshealth;

    private void Start()
    {
        statholder = Body.GetComponent<StatHolder>();
        bodyAnimator = Body.GetComponent<Animator>();
        handsAnimator = Hands.GetComponent<Animator>();

        Attack attack = statholder.FindPropertyByName("Attack") as Attack;
        bosshealth = statholder.FindPropertyByName("Health") as Health;

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Defence defence = Player.GetComponent<StatHolder>().FindPropertyByName("Defence") as Defence;
        attack.runtimeBaseValue = defence.runtimeBaseValue + 1;
    }

    private void Update()
    {
        if(!bosshealth.Dead)
        {
            if (canAttack)
            {
                GetAvailebleAttacks();
                Attack();
            }

            if (punching)
            {
                Punch();
            }
        }
        else
        {
            RightHand.SetActive(false);
            LeftHand.SetActive(false);
        }
    }

    private void GetAvailebleAttacks()
    {
        AvailebleAttacks.Clear();

        BossAttack highestPriorityAttack = null;

        if(RightHand == null && LeftHand == null)
        {
            triggers.Remove(triggers.Find(t => t.bossAttack.name == "Hand swing"));
            triggers.Remove(triggers.Find(t => t.bossAttack.name == "Punch"));
        }

        foreach (BossTrigger trigger in triggers)
        {
            if (trigger.PlayerInTrigger)
            {
                if (highestPriorityAttack == null)
                {
                    highestPriorityAttack = trigger.bossAttack;
                }

                if(trigger.bossAttack.Priority > highestPriorityAttack.Priority)
                {
                    AvailebleAttacks.Add(trigger.bossAttack);
                }
                else if(trigger.bossAttack.Priority == highestPriorityAttack.Priority)
                {
                    AvailebleAttacks.Add(trigger.bossAttack);
                }
            }
        }
    }

    private System.Random random = new System.Random();

    private void Attack()
    {
        if(AvailebleAttacks.Count != 0)
        {
            BossAttack chosenAttack = AvailebleAttacks[random.Next(0, AvailebleAttacks.Count)];

            switch (chosenAttack.name)
            {
                case "Hand swing":
                    HandSwing();
                    break;
                case "Fireball":
                    Fireball();
                    break;
                case "Punch":
                    Punch();
                    break;
            }
        }
    }

    private void HandSwing()
    {
        StartCoroutine(AwaitAttackCooldown());
        StartCoroutine(SwingHandsForSetSeconds());
    }

    private IEnumerator SwingHandsForSetSeconds()
    {
        handsAnimator.SetBool("Swing attack", true);
        yield return new WaitForSeconds(2);
        handsAnimator.SetBool("Swing attack", false);
    }
    
    private void Fireball()
    {
        StartCoroutine(AwaitAttackCooldown());
        BossFire.GetComponent<BossFireAttack>().ProjectileSpawner = ProjectileLauncher;
        GameObject bossfire = Instantiate(BossFire, ProjectileLauncher.transform);
    }

    public float HandSpeed;

    public Vector2 startMarkerRightHand;
    public Vector2 startMarkerLeftHand;
    public Vector2 LefthandendMarker;
    public Vector2 RightHandendMarker;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLengthLefthand;
    private float journeyLengthRighthand;

    private void Punch()
    {
        if(RightHand != null && LeftHand != null)
        {
            if (!punching)
            {
                StartCoroutine(AwaitAttackCooldown());
                StartCoroutine(AwaitPunch());
                startMarkerRightHand = new Vector2(RightHand.transform.position.x, RightHand.transform.position.y);
                startMarkerLeftHand = new Vector2(LeftHand.transform.position.x, LeftHand.transform.position.y);

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    LefthandendMarker = new Vector2(player.transform.position.x - 1f, player.transform.position.y);
                    RightHandendMarker = new Vector2(player.transform.position.x + 1f, player.transform.position.y);
                    startTime = Time.time;

                    // Calculate the journey length.
                    journeyLengthLefthand = Vector3.Distance(startMarkerLeftHand, LefthandendMarker);
                    journeyLengthRighthand = Vector3.Distance(startMarkerRightHand, RightHandendMarker);
                }
            }

            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * HandSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourneyRightHand = distCovered / journeyLengthRighthand;
            float fractionOfJourneyLeftHand = distCovered / journeyLengthLefthand;

            // Set our position as a fraction of the distance between the markers.
            RightHand.transform.position = Vector2.Lerp(startMarkerRightHand, LefthandendMarker, fractionOfJourneyRightHand);
            LeftHand.transform.position = Vector2.Lerp(startMarkerLeftHand, RightHandendMarker, fractionOfJourneyLeftHand);
        }
    }

    private IEnumerator AwaitPunch()
    {
        punching = true;
        handsAnimator.enabled = false;
        yield return new WaitForSeconds(2f);
        punching = false;
        handsAnimator.enabled = true;
        LeftHandParticles.Play();
        RightHandParticles.Play();
    }

    private IEnumerator AwaitAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(SecondsBetweenAttack);
        canAttack = true;
    }

}

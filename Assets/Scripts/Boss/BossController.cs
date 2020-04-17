using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private StatHolder statholder;

    public BossTrigger SwingAttack;
    public BossTrigger fireball;
    public BossTrigger punch;

    public GameObject Arms;
    public GameObject Body;

    private Animator bodyAnimator;
    private Animator armsAnimator;

    public GameObject ProjectileLauncher;

    private void Start()
    {
        statholder = Body.GetComponent<StatHolder>();
        bodyAnimator = Body.GetComponent<Animator>();
        armsAnimator = Arms.GetComponent<Animator>();
    }

    private void Update()
    {
        
    }



}

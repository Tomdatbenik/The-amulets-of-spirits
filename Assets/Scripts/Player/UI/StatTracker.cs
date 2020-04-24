using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTracker : MonoBehaviour
{
    public StatHolder statHolder;

    public Text HealthText;
    public Text DefenceText;
    public Text SpeedText;
    public Text AttackText;

    Health health;
    Defence defence;
    Speed speed;
    Attack attack;

    private void Start()
    {
        health = statHolder.FindPropertyByName("Health") as Health;
        defence = statHolder.FindPropertyByName("Defence") as Defence;
        speed = statHolder.FindPropertyByName("Speed") as Speed;
        attack = statHolder.FindPropertyByName("Attack") as Attack;
    }

    public void Update()
    {
        if (HealthText != null)
        {
            HealthText.text = "Health: " + health.Value.ToString();
            DefenceText.text = "Defence: " + defence.Value.ToString() + " + " + (defence.Value - defence.baseValue);
            SpeedText.text = "Speed: " + speed.Value.ToString() + " + " + (speed.Value - speed.baseValue);
            AttackText.text = "attack: " + attack.Value.ToString() + " + " + (attack.Value - attack.baseValue);
        }
    }
}

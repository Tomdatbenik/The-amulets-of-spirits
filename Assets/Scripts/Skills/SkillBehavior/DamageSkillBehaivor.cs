using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class DamageSkillBehaivor : SkillBehavior
{
    public GameObject hitbox;
    GameObject SpawnedHitbox;
    public float offsetOfCaster;

    protected override void Act()
    {
        SpawnedHitbox = Instantiate(hitbox);
        HitBoxApplyDamage hitBoxApplyDamage = SpawnedHitbox.GetComponent<HitBoxApplyDamage>();
        hitBoxApplyDamage.attack = Caster.GetComponent<StatHolder>().FindPropertyByName("Attack") as Attack;
        hitBoxApplyDamage.tags = ApplyToTags;

        SetHitBoxPosiition();
    }

    private void SetHitBoxPosiition()
    {
        SpawnedHitbox.transform.position = TargetPosition;
    }

    public override void Reset()
    {
        Destroy(SpawnedHitbox);
    }

    public override void Preform()
    {
        Act();
        IsActing = true;
        Endtime = Time.time + duration;
    }

    public override void Update()
    {
        if(IsActing)
        {
            SetHitBoxPosiition();
            if (Time.time > Endtime)
            {
                IsActing = false;
                Reset();
            }
        }
    }
}

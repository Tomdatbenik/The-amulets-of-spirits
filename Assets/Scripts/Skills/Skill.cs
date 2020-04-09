using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class Skill : ScriptableObject
{
    public List<SkillBehavior> behaviors;
    private List<SkillBehavior> behaviorsCopy = new List<SkillBehavior>();
    public float Cooldown;
    public bool onCooldown;
    public Vector2 TargetPosition;
    public GameObject Caster;
    public float Range;

    private float endTime;

    public bool Casting()
    {
        foreach (SkillBehavior behavior in behaviorsCopy)
        {
            if(behavior.IsActing)
            {
                return true;
            }
        }

        return false;
    }

    public void Cast()
    {
        if(!onCooldown)
        {
            onCooldown = true;
            endTime = Time.time + Cooldown;
            foreach (SkillBehavior behavior in behaviors)
            {
                behaviorsCopy.Add(Instantiate(behavior));
            }

            foreach (SkillBehavior behavior in behaviorsCopy)
            {
                behavior.Caster = Caster;
                behavior.TargetPosition = TargetPosition;
                behavior.Preform();
            }
        }
    }

    public void Update()
    {
        if(onCooldown)
        {
            if(Time.time > endTime)
            {
                onCooldown = false;
            }
        }

        for (int i = 0; i < behaviorsCopy.Count; i++)
        {
            SkillBehavior behavior = behaviorsCopy[i];
            behavior.Update();
            if (!behavior.IsActing)
            {
                behavior.Reset();
                behaviorsCopy.RemoveAt(i);
            }
        }
    }
}

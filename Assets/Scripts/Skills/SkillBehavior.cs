using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBehavior : ScriptableObject
{
    public List<string> ApplyToTags;

    public GameObject Caster;

    public float duration;
    public Vector2 TargetPosition;

    protected float Endtime;
    public bool IsActing;

    public abstract void Preform();

    protected abstract void Act();

    public abstract void Reset();

    public abstract void Update();
}

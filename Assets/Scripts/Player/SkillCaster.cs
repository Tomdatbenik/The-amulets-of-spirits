using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    public List<Skill> ActingSkills = new List<Skill>();

    public Skill primarySkill;
    Skill primarySkillClone;

    private void Start()
    {
        primarySkillClone = Instantiate(primarySkill);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CastPrimarySkill();
        }

        List<Skill> finishedSkills = new List<Skill>();

        for (int i = 0; i < ActingSkills.Count; i++)
        {
            Skill skill = ActingSkills[i];
            skill.Update();
            if(!skill.Casting())
            {
                finishedSkills.Add(skill);
            }
        }

        foreach(Skill finishedSkill in finishedSkills)
        {
            ActingSkills.Remove(finishedSkill);
        }
    }

    private void CastPrimarySkill()
    {
        if(!primarySkillClone.onCooldown)
        {
            ActingSkills.Add(primarySkillClone);
            primarySkillClone.Caster = this.gameObject;

            //Vector2 MousePos = transform.TransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //Vector2 test = MousePos;
            //if (Vector3.Distance(MousePos, this.transform.position) > primarySkillClone.Range)
            //{
            //    test = (MousePos - (Vector2)transform.position).normalized * primarySkillClone.Range + (Vector2)transform.position;
            //}

            primarySkillClone.TargetPosition = new Vector2(transform.position.x, transform.position.y);
            primarySkillClone.Cast();
        }
    }
}

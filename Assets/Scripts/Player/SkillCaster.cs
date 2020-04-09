using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    public List<Skill> skills;
    public List<Skill> ActingSkills = new List<Skill>();

    Skill primarySkill;

    private void Start()
    {
        primarySkill = Instantiate(skills[0]);
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
        ActingSkills.Add(primarySkill);
        primarySkill.Caster = this.gameObject;
   
        Vector2 MousePos = transform.TransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2 test = MousePos;
        if (Vector3.Distance(MousePos, this.transform.position) > primarySkill.Range)
        {
            test = (MousePos - (Vector2)transform.position).normalized * primarySkill.Range + (Vector2)transform.position;
        }
       
        primarySkill.TargetPosition = test;
        primarySkill.Cast();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class ModifierBehavior : ScriptableObject
{
    public string Name;
    public string Description;

    public bool ActOnce;

    private bool Acted = false;
    public bool FinishedActing = false;

    public List<int> ActMultipleTimes;
    private int index = 0;
    public float Duration;
    private float EndTime;
    private float StartTime;

    public int IncreaseAmount;
    public int DecreaseAmount;

    public bool ResetWhenFinished;

    public Property AffectedProperty;

    public void Act()
    {
        if(ActOnce)
        {
            PreformBehaivorOnce();
        }
        else
        {
            PreformBehaivorOverTime();
        }
    }

    private void PreformBehaivorOverTime()
    {
        if(!Acted)
        {
            Acted = true;
            EndTime = Time.time + Duration;
            StartTime = Time.time;
      
        }

        if(!FinishedActing)
        {
            float percentageOfDuration = (100 / (EndTime - StartTime) * (Time.time - StartTime));
            if(ActMultipleTimes.Count != index)
            {
                if (percentageOfDuration > ActMultipleTimes[index])
                {
                    index++;

                    AffectedProperty.Increase(IncreaseAmount);
                    AffectedProperty.Decrease(DecreaseAmount);
                }
            }
        }

        CheckIfEnded();
    }

    private void PreformBehaivorOnce()
    {
        if (!Acted)
        {
            Acted = true;
   
            EndTime = Time.time + Duration;

            AffectedProperty.Increase(IncreaseAmount);
            AffectedProperty.Decrease(DecreaseAmount);
        }

        CheckIfEnded();
    }


    public void StopAffect()
    {
        AffectedProperty.Decrease(IncreaseAmount);
        AffectedProperty.Increase(DecreaseAmount);
    }

    protected void CheckIfEnded()
    {
        if (Time.time > EndTime)
        {
            FinishedActing = true;
            if(ResetWhenFinished)
            {
                StopAffect();
            }
        }
    }


}

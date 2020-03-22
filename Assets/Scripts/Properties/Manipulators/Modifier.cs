using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : ScriptableObject
{
    #region Info
    public string Name;
    [TextArea]
    public string Description;
    #endregion

    #region duration
    public float Duration;
    private float EndTime;
    #endregion

    #region property
    //public PropertyType PropertyType;
    public Property property;
    public int ModifyAmount;
    public ModifierType Modifiertype;
    #endregion

    #region Affect
    protected bool InAffect = false;
    public bool Acted = false;
    public bool FinishedActing = false;
    #endregion

    //Insert behaivor stuff
    public void Act()
    {
        InAffect = true;

        if (InAffect)
        {
            if (!Acted)
            {
                EndTime = Time.time + Duration;
                Acted = true;

                if (Modifiertype == ModifierType.BUFF)
                {
                    property.Increase(ModifyAmount);
                }
                else
                {
                    property.Decrease(ModifyAmount);
                }
                Debug.Log("Acted " + name);
            }
            CheckIfEnded();
        }
    }

    public void StopAffect()
    {
        if (Modifiertype == ModifierType.BUFF)
        {
            property.Decrease(ModifyAmount);
        }
        else
        {
            property.Increase(ModifyAmount);
        }
    }

    protected void CheckIfEnded()
    {
        if(Time.time > EndTime)
        {
            Debug.Log("Ended");
            FinishedActing = true;

            StopAffect();
        }
    }
}

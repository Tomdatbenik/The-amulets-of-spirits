using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class Health : Property
{
    public Defence defence;

    public bool Dead
    {
        get
        {
            if (Value <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        set
        {
            if (value)
            {
                Value = 0;
            }
        }
    }

    #region apply damage
    //May delight one of the methods later on
    public void ApplyDamage(Damage damage)
    {
        int dmg = damage.Value;
        ///Might balance this with with defence devided by 75% or something
        if (defence != null)
        {
            dmg = damage.Value - defence.Value;
        }
    

        if(dmg <= 0)
        {
            dmg = 1;
        }

        Decrease(dmg);
    }

    public void ApplyDamage(int damage)
    {
        ///Might balance this with with defence devided by 75% or something
        int dmg = damage;
        ///Might balance this with with defence devided by 75% or something
        if (defence != null)
        {
            dmg = damage - defence.Value;
        }

        if (dmg <= 0)
        {
            dmg = 1;
        }

        Decrease(dmg);
    }

    public void ApplyDamage(Attack attack)
    {
        ///Might balance this with with defence devided by 75% or something
        int dmg = attack.Value;

        if (defence != null)
        {
            dmg = attack.Value - defence.Value;
        }
        if (dmg <= 0)
        {
            dmg = 1;
        }

        Decrease(dmg);
    }
    #endregion



}

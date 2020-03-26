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

    #region property
    //public PropertyType PropertyType;
    public Property property;
    #endregion

    #region Affect
    protected bool InAffect = false;
    public bool Acted = false;
    public bool FinishedActing = false;
    #endregion

    public List<ModifierBehaivor> Behaivors;
    private List<ModifierBehaivor> BehaivorsClones = new List<ModifierBehaivor>();

    //Insert behaivor stuff
    public void Act()
    {
        if(!Acted)
        {
            Acted = true;

            foreach (ModifierBehaivor behaivor in Behaivors)
            {
                ModifierBehaivor ClonedBehaiver = Instantiate(behaivor);

                BehaivorsClones.Add(ClonedBehaiver);
            }
        }


        InAffect = true;

        if (InAffect)
        {
            foreach (ModifierBehaivor behaivor in BehaivorsClones)
            {
                behaivor.Act();
            }

            CheckAndSetEnded();
        }
    }

    protected void CheckAndSetEnded()
    {
        bool ended = true;
        foreach(ModifierBehaivor behaivor in BehaivorsClones)
        {
            if(!behaivor.FinishedActing)
            {
                ended = false;
            }
        }

        if(ended)
        {
            FinishedActing = true;
        }
    }
}

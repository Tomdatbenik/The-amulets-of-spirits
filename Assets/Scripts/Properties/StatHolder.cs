using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHolder : MonoBehaviour
{
    public List<Property> properties = new List<Property>();
    public GameObject EffectObject;

    private void FixedUpdate()
    {
        ActModifiers();
        RemoveFinishedModifiers();
    }

    private void ActModifiers()
    {
        foreach(Property property in properties)
        {
            if(property.Modifiers.Count == 0)
            {
                property.RestoreBackToInit();
            }

            foreach(Modifier modifier in property.Modifiers)
            {
                modifier.Act();
            }
        }
    }

    private void RemoveFinishedModifiers()
    {
        foreach (Property property in properties)
        {
            for (int i = 0; i < property.Modifiers.Count; i++)
            {
                Modifier modifier = property.Modifiers[i];

                if (modifier.FinishedActing)
                {
                    if(modifier.effect != null)
                    {
                        Destroy(modifier.effect);
                    }
               
                    Destroy(modifier);

                    property.RemoveModifier(modifier);
                }
            }
        }
    }

    public void AddModifier(Modifier modifier)
    {
        Modifier newModifier = Instantiate(modifier);

        foreach(Property property in properties)
        {
            if(newModifier.property.GetType() == property.GetType())
            {
                if(newModifier.effect != null)
                {
                    newModifier.effect = Instantiate(modifier.effect, EffectObject.transform);
                }

                newModifier.property = property;
                property.ApplyModifier(newModifier);

                foreach(ModifierBehavior behaivor in newModifier.Behaivors)
                {
                    behaivor.AffectedProperty = property;
                }
            }
        }
    }
}

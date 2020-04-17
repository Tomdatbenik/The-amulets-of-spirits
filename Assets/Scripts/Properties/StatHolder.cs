using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHolder : MonoBehaviour
{
    public List<Property> properties = new List<Property>();
    public GameObject EffectObject;
    private Color color;
    public AudioSource audioSource;

    private void Awake()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
        List<Property> propertiesClones = new List<Property>();

        foreach(Property property in properties)
        {
            propertiesClones.Add(Instantiate(property));
        }

        properties.Clear();

        properties = propertiesClones;
    }

    private void Update()
    {
        Health health = FindPropertyByName("Health") as Health;

        if(health.Value == 0)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Soundplayer");
            if(gameObject != null)
            {
                AudioSource deathsource = gameObject.GetComponent<AudioSource>();
                if (deathsource != null)
                {
                    deathsource.clip = health.DeathSound;
                    deathsource.Play();
                }
            }
          
  
            Destroy(this.gameObject);
        }
    }

    public Property FindPropertyByName(string name)
    {
        return properties.Find(p => p.GetType().Name == name);
    }

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
                if(!property.isState)
                {
                    property.RestoreBackToRuntimeBaseValue();
                }
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

    public void DisplayDamage(SpriteRenderer renderer)
    {
        StartCoroutine(SetAndResetColor(renderer));
    }

    private IEnumerator SetAndResetColor(SpriteRenderer renderer)
    {
        renderer.color = Color.black;
        yield return new WaitForSeconds(0.01f);
        renderer.color = color;
    }
}

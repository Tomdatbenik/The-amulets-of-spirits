using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePropertyDrop : MonoBehaviour
{
    public List<Property> properties;
    public List<string> tags;
    public int IncreaseAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tags.Contains(collision.tag))
        {
            StatHolder statHolder = collision.GetComponent<StatHolder>();
            System.Random random = new System.Random();
            Property p = statHolder.FindPropertyByName(properties[random.Next(0,properties.Count)].GetType().Name);
            p.IncreaseRuntimeBaseValue(IncreaseAmount);
            Destroy(gameObject);
        }
    }
}

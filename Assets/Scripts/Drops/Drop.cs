using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Modifier modifier;
    public List<string> tags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tags.Contains(collision.tag))
        {
            StatHolder statHolder = collision.GetComponent<StatHolder>();
            statHolder.AddModifier(modifier);

            Destroy(gameObject);
        }
    }


}

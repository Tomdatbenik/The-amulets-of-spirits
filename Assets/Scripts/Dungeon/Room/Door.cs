using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite DoorGoesTo;
    public Sprite ClosedDoor;

    public bool CanOpen;

    public GameObject doorObject;

    public void Open()
    {
        CanOpen = true;
        doorObject.GetComponent<SpriteRenderer>().sprite = DoorGoesTo;
        doorObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void Close()
    {
        doorObject.GetComponent<SpriteRenderer>().sprite = ClosedDoor;
        doorObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}

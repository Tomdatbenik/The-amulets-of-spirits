using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettings : MonoBehaviour
{
    public Door LeftDoor;
    public Door RightDoor;
    public Door TopDoor;
    public Door BottomDoor;

    public Transform LeftDoorDestination;
    public Transform RightDoorDestination;
    public Transform BottomDoorDestination;
    public Transform TopDoorDestination;

    public List<GameObject> Monsters;
    public bool RoomCleared;
    public int MaxMonsters;
    public int MinMonsters;

    public Sprite RoomDoorSprite;
    public Sprite DoorClosed;

    public Color color;

    public void SetUpDoor(Door door, RoomSettings otherSettings)
    {
        if(otherSettings.RoomDoorSprite != null)
        {
            door.DoorGoesTo = otherSettings.RoomDoorSprite;
            door.doorObject.GetComponent<SpriteRenderer>().color = otherSettings.color;
            door.DoorGoesTo = otherSettings.RoomDoorSprite;
        }

        door.ClosedDoor = otherSettings.DoorClosed;

        door.Open();
    }

    public void CloseDoor(Door door)
    {
        door.Close();
    }
}

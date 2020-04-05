using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTransferPlayer : MonoBehaviour
{
    public RoomSettings settings;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovePlayer(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        MovePlayer(collision);
    }

    private void MovePlayer(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TeleportingPlayer tp = collision.gameObject.GetComponent<TeleportingPlayer>();
            if (!tp.Teleporting)
            {
                tp.Teleporting = true;
                tp.Teleport();

                switch (this.gameObject.tag)
                {
                    case "LeftDoor":
                        collision.gameObject.transform.position = settings.LeftDoorDestination.position;
                        break;
                    case "RightDoor":
                        collision.gameObject.transform.position = settings.RightDoorDestination.position;
                        break;
                    case "BottomDoor":
                        collision.gameObject.transform.position = settings.BottomDoorDestination.position;
                        break;
                    case "TopDoor":
                        collision.gameObject.transform.position = settings.TopDoorDestination.position;
                        break;
                }
            }
        }
    }
}

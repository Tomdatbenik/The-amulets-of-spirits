using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class RoomList : ScriptableObject
{
    public List<Room> rooms;

    public Room GetStartRoom()
    {
        return rooms.Find(r => r.type == RoomType.START);
    }

    public Room GetBossRoom()
    {
        return rooms.Find(r => r.type == RoomType.BOSS);
    }

    public Room GetPuzzleRoom()
    {
        return rooms.Find(r => r.type == RoomType.PUZZLE);
    }

    public Room GetNormalRoom()
    {
        System.Random random = new System.Random();

        List<Room> availableRooms = GetNormalRooms();

        if(availableRooms.Count != 0)
        {
            int index = random.Next(0, availableRooms.Count);

            return availableRooms[index];
        }
        else
        {
            return null;
        }
       
    }

    private List<Room> GetNormalRooms()
    {
        return rooms.FindAll(r => r.type == RoomType.NORMAL);
    }
}

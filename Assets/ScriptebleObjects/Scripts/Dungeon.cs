using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : ScriptableObject
{
    public int dungeonSize;

    public RoomList roomList;

    public List<DungeonRoom> rooms;

    private System.Random random = new System.Random();

    public void GenerateDungeon()
    {
        int roomIndex = 0;

        while(rooms.Count < dungeonSize)
        {
            if(rooms.Count == 0)
            {
                CreateStartRoom();
                roomIndex++;
            }
            else
            {
                bool canGenerate = false;
                int tries = 0;
                while (!canGenerate)
                {
                    canGenerate = TryGenerateRoom(rooms[roomIndex]);
                    tries++;

                    if (tries == 3)
                    {
                        roomIndex--;
                        break;
                    }
                }
            }
        }
    }

    private void CreateStartRoom()
    {
        DungeonRoom dungeonRoom = new DungeonRoom();
        dungeonRoom.location = new Vector2(0, 0);
        dungeonRoom.room = roomList.GetStartRoom();

        rooms.Add(dungeonRoom);
    }

    public bool TryGenerateRoom(DungeonRoom dungeonRoom)
    {
        DungeonRoom newRoom = new DungeonRoom();

        newRoom.location = dungeonRoom.location;
        SetRandomLocationFromPoint(newRoom);

        int tries = 0;
        bool canGenerateRoom = LocationAvailable(newRoom.location);
        while (!canGenerateRoom)
        {
            canGenerateRoom = LocationAvailable(newRoom.location);
            SetRandomLocationFromPoint(newRoom);
            tries++;

            if(tries == 3)
            {
                break;
            }
        }

        if(canGenerateRoom)
        {
            rooms.Add(newRoom);
        }

        return canGenerateRoom;
    }

    public void SetRandomLocationFromPoint(DungeonRoom room)
    {
        float roomX = room.location.x;
        float roomY = room.location.y;

        while(room.location.x == roomX)
        {
            room.location.x += random.Next(-1, 2);
        }

        while (room.location.y == roomY)
        {
            room.location.y += random.Next(-1, 2);
        }
    }

    public bool LocationAvailable(Vector2 location)
    {
        foreach(DungeonRoom room in rooms)
        {
            if(location == room.location)
            {
                return false;
            }
        }

        return true;
    }
}

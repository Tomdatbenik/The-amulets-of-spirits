using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    private Grid grid = new Grid();

    public int dungeonSize;

    public float SpaceBetweenRooms;

    public DungeonRoom startRoom;
    public DungeonRoom BossRoom;
    public List<DungeonRoom> rooms;

    private System.Random random = new System.Random();


    int index = 0;
    private bool DoorSpawned = false;
    
    void Start()
    {
        grid.spaces[0].dungeonRoom = Instantiate(startRoom);
        grid.spaces[0].dungeonRoom.Name = "StartRoom";

        for (int i = 1; i < dungeonSize; i++)
        {
            grid.spaces.Add(grid.GetAvailebleSpace());
            grid.spaces[grid.spaces.Count - 1].dungeonRoom = Instantiate(rooms[random.Next(0, rooms.Count)]);
        }

        grid.spaces.Add(grid.GetAvailebleSpace());
        grid.spaces[grid.spaces.Count - 1].dungeonRoom = Instantiate(BossRoom);


        //foreach (GridSpace space in grid.spaces)
        //{
        //    SpawnRoom(space);

        //    space.dungeonRoom.Room.transform.position = new Vector2(space.position.x * SpaceBetweenRooms, space.position.y * SpaceBetweenRooms);
        //}

        //SpawnDoors();
    }

    private void SpawnRoom(GridSpace space)
    {
        GameObject room = Instantiate(space.dungeonRoom.Room);
        space.dungeonRoom.Room = room;
        space.dungeonRoom.Name = "Randomroom";

        room.transform.position = new Vector2(space.position.x * SpaceBetweenRooms, space.position.y * SpaceBetweenRooms);
    }


    private void Update()
    {

        if (index != grid.spaces.Count)
        {
            GridSpace gridSpace = grid.spaces[index];

            SpawnRoom(gridSpace);

            index++;
        }
        else if (index == grid.spaces.Count && !DoorSpawned)
        {
            DoorSpawned = true;
            SpawnDoors();
        }

    }

    private void SpawnDoors()
    {
        foreach (GridSpace space in grid.spaces)
        {
            RoomSettings settings = space.dungeonRoom.Room.GetComponent<RoomSettings>();

            List<GridSpace> spaces = grid.GetAdjecentSpacesWithRoomFromSpace(space);

            foreach (GridSpace GSpace in spaces)
            {
                RoomSettings GSpaceRoomSetting = GSpace.dungeonRoom.Room.GetComponent<RoomSettings>();
                if (GSpace.position.x > space.position.x)
                {
                    settings.SetUpDoor(settings.RightDoor, GSpaceRoomSetting);
                    settings.RightDoorDestination = GSpaceRoomSetting.LeftDoor.transform;
                }

                if (GSpace.position.x < space.position.x)
                {
                    settings.SetUpDoor(settings.LeftDoor, GSpaceRoomSetting);
                    settings.LeftDoorDestination = GSpaceRoomSetting.RightDoor.transform;
                }

                if (GSpace.position.y > space.position.y)
                {
                    settings.SetUpDoor(settings.TopDoor, GSpaceRoomSetting);
                    settings.TopDoorDestination = GSpaceRoomSetting.BottomDoor.transform;
                }

                if (GSpace.position.y < space.position.y)
                {
                    settings.SetUpDoor(settings.BottomDoor, GSpaceRoomSetting);
                    settings.BottomDoorDestination = GSpaceRoomSetting.TopDoor.transform;
                }
            }
        }
    }
}

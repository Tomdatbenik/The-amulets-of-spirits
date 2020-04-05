using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace
{
    public GridPosition position;

    public DungeonRoom dungeonRoom;

    public GridSpace()
    {

    }

    public GridSpace(GridPosition position)
    {
        this.position = position;
    }

    public GridSpace(float xPos, float yPos)
    {
        position = new GridPosition(xPos, yPos);
    }
}

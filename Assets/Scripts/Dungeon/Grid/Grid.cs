using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    private System.Random random = new System.Random();

    public Grid()
    {
        spaces.Add(new GridSpace(0,0));
    }

    public List<GridSpace> spaces = new List<GridSpace>();

    public GridSpace GetAvailebleSpace()
    {
        List<GridSpace> availableSpaces = new List<GridSpace>();

        while(availableSpaces.Count == 0)
        {
            GridSpace space = GetSpaceAtEnd();

            availableSpaces = GetAvailebleAdjecentSpaces(space);

            if(availableSpaces.Count == 0)
            {
                space = GetRandomSpace();

                availableSpaces = GetAvailebleAdjecentSpaces(space);
            }
        }

        return availableSpaces[random.Next(0, availableSpaces.Count)];
    }

    private GridSpace GetRandomSpace()
    {
        return spaces[random.Next(0, spaces.Count)];
    }

    private GridSpace GetSpaceAtEnd()
    {
        if(spaces.Count > 1)
        {
            return spaces[spaces.Count-1];
        }
        else
        {
            return spaces[random.Next(0, spaces.Count)];
        }
    }

    private List<GridSpace> GetAvailebleAdjecentSpaces(GridSpace space)
    {
        List<GridSpace> availableSpaces = new List<GridSpace>();

        List<GridPosition> checkPositions = GetAdjecentPositionsFromSpace(space);

        foreach (GridSpace gSpace in spaces)
        {
            for(int index = 0; index < checkPositions.Count; index++)
            {
                if(gSpace.position.x == checkPositions[index].x && gSpace.position.y == checkPositions[index].y)
                {
                    checkPositions.RemoveAt(index);
                }
            }
        }

        foreach(GridPosition position in checkPositions)
        {
            availableSpaces.Add(new GridSpace(position));
        }

        return availableSpaces;
    }

    public List<GridPosition> GetAdjecentPositionsFromSpace(GridSpace space)
    {
        List<GridPosition> positions = new List<GridPosition>();

        //left
        positions.Add(new GridPosition(space.position.x - 1, space.position.y));
        //up
        positions.Add(new GridPosition(space.position.x, space.position.y + 1));
        //right
        positions.Add(new GridPosition(space.position.x + 1, space.position.y));
        //down
        positions.Add(new GridPosition(space.position.x, space.position.y - 1));

        return positions;
    }

    public List<GridSpace> GetAdjecentSpacesWithRoomFromSpace(GridSpace space)
    {
        List<GridPosition> positions = GetAdjecentPositionsFromSpace(space);

        List<GridSpace> adjecentSpaces = new List<GridSpace>();

        foreach(GridSpace GSpace in spaces)
        {
            foreach(GridPosition position in positions)
            {
                if (GSpace.position.x == position.x && GSpace.position.y == position.y)
                {
                    if(GSpace.dungeonRoom != null)
                    {
                        adjecentSpaces.Add(GSpace);
                    }
                }
            }
        }

        return adjecentSpaces;
    }
    
}

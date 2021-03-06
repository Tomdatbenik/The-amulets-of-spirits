﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    public RoomSettings settings;
    public GameObject monsterHolder;

    private System.Random random = new System.Random();

    private bool monstersSpawned = false;
    public List<Transform> SpawnLocations;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!settings.RoomCleared)
        {
            if(!monstersSpawned)
            {
                monstersSpawned = true;
                int ammountOfMonsters = random.Next(settings.MinMonsters, settings.MaxMonsters + 1);

                for(int i = 0; i < ammountOfMonsters; i++)
                {
                    GameObject monster = Instantiate(settings.Monsters[random.Next(0, settings.Monsters.Count)], monsterHolder.transform);

                    monster.transform.position = SpawnLocations[random.Next(0, SpawnLocations.Count)].position;
                }

                settings.RightDoor.Close();
                settings.LeftDoor.Close();
                settings.TopDoor.Close();
                settings.BottomDoor.Close();
            }
        }
    }

    private void LateUpdate()
    {
        if(monstersSpawned && !settings.RoomCleared)
        {
            if (monsterHolder.transform.childCount == 0)
            {
                settings.RoomCleared = true;

                if (settings.RightDoor.CanOpen)
                {
                    settings.RightDoor.Open();
                }
                if (settings.LeftDoor.CanOpen)
                {
                    settings.LeftDoor.Open();
                }
                if (settings.TopDoor.CanOpen)
                {
                    settings.TopDoor.Open();
                }
                if (settings.BottomDoor.CanOpen)
                {
                    settings.BottomDoor.Open();
                }
            }
        }
    }
}

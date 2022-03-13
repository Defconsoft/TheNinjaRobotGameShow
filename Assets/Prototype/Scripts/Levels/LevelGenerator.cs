using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject roomPrefabs;

    public int numberOfRooms;
    public Transform spawnPoint;

    public GameObject envContainer;



    // Start is called before the first frame update
    void Start()
    {
        GameObject startPlatform = Instantiate(startPrefab, transform.position, Quaternion.identity);
        startPlatform.transform.parent = envContainer.transform;
        SetNewSpawnPoint(startPlatform.transform);
        SpawnRooms();
    }

    private void SpawnRooms()
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            GameObject levelRoom = Instantiate(roomPrefabs, spawnPoint);
            levelRoom.transform.parent = envContainer.transform;
            SetNewSpawnPoint(levelRoom.transform);
        }

        GameObject endRoom = Instantiate(endPrefab, spawnPoint);
        endRoom.transform.parent = envContainer.transform;

    }

    private void SetNewSpawnPoint(Transform parent) {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == "Env_End")
            {
                spawnPoint = child.transform;
            }
        }
    }

}

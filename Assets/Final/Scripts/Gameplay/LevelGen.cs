using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGen : MonoBehaviour
{

    [Header("Components")]
    public GameObject[] roomPrefabs;
    public GameObject startObject;
    public GameObject levelContainer;
    public GameObject levelEnd;
    public Transform spawnPoint;
    public GameObject envContainer;
    public int roomNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        SetNewSpawnPoint(startObject.transform);
        GenerateNextRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateNextRoom() {
        //first spawn a container
        GameObject nextContainer = Instantiate(levelContainer, spawnPoint);
        //move its parent
        nextContainer.transform.parent = envContainer.transform;
        GameObject nextNumber = nextContainer.transform.Find("RoomNumberCanvas").gameObject.transform.Find("RoomNumber").gameObject;
        ChangeRoomNumber(nextNumber);
        //move the spawnpoint
        SetNewSpawnPoint(nextContainer.transform);

        //then spawn a level room
        GameObject levelRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], spawnPoint);
        //move its parent
        levelRoom.transform.parent = envContainer.transform;
        //move the spawnpoint
        SetNewSpawnPoint(levelRoom.transform);

        //Generate the level end
        GameObject nextEnd = Instantiate(levelEnd , spawnPoint);
        //move its parent
        nextEnd.transform.parent = envContainer.transform;
        //move the spawnpoint
        SetNewSpawnPoint(nextEnd.transform);
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

    private void ChangeRoomNumber(GameObject numberContainer){
        roomNumber++;
        numberContainer.GetComponent<TMP_Text>().text = roomNumber.ToString();
    }



}

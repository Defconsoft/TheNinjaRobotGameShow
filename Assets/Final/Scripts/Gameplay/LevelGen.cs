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
    public int NextRoom;
    public int roomNumber = 0;
    public int LastRoom;


    // Start is called before the first frame update
    void Start()
    {
        SetNewSpawnPoint(startObject.transform);
        GenerateNextRoom();
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

        
        GenerateMainRoom();
/*         //then spawn a level room
        NextRoom = Random.Range(0, roomPrefabs.Length);
        GameObject levelRoom = Instantiate(roomPrefabs[NextRoom], spawnPoint);
        //move its parent
        levelRoom.transform.parent = envContainer.transform;
        //move the spawnpoint
        SetNewSpawnPoint(levelRoom.transform); */

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


    public void GenerateMainRoom(){
        switch(LastRoom) {
            case 0: 
                NextRoom = Random.Range(1, 3);
                LastRoom = NextRoom;
                GameObject levelRoom = Instantiate(roomPrefabs[NextRoom], spawnPoint);
                //move its parent
                levelRoom.transform.parent = envContainer.transform;
                //move the spawnpoint
                SetNewSpawnPoint(levelRoom.transform);
            break;

            case 1: 
                float tmpChance = Random.Range (0,1f);
                if (tmpChance >= 0.5f){
                    NextRoom = 0;
                    LastRoom = NextRoom;
                } else {
                    NextRoom = 2;
                    LastRoom = NextRoom;
                }
                GameObject levelRoom1 = Instantiate(roomPrefabs[NextRoom], spawnPoint);
                //move its parent
                levelRoom1.transform.parent = envContainer.transform;
                //move the spawnpoint
                SetNewSpawnPoint(levelRoom1.transform);
            break;

            case 2: 
                NextRoom = Random.Range(0, 2);
                LastRoom = NextRoom;
                GameObject levelRoom2 = Instantiate(roomPrefabs[NextRoom], spawnPoint);
                //move its parent
                levelRoom2.transform.parent = envContainer.transform;
                //move the spawnpoint
                SetNewSpawnPoint(levelRoom2.transform);
            break;

        }
    }



}

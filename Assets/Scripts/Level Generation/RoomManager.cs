using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    public GameObject levelGenerator;
    public static RoomManager instance;

    private GameObject[,] rooms;

    private GameObject[,] mapRooms;

    private Vector2 currentRoom;
    private Vector2 previousRoom;

    public GameObject player;
    public Camera cam;

    public Tilemap grassTilemap, wallTilemap, ledgeTilemap;

    private AudioSource soundtrackAudioSource, soundtrackAudioSource2, soundtrackAudioSource3;

    public void Start()
    {
        instance = this;
        rooms = new GameObject[20,20];
        mapRooms = new GameObject[20,20];
        currentRoom = new Vector2(10,10);

        soundtrackAudioSource = SoundManager.PlaySoundTrack(SoundManager.Sound.homeTrack);

    }


    public void MoveRoomDown()
    {
        
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (rooms[x, y] != null) { 
                    rooms[x,y].transform.position = new Vector2(rooms[x,y].transform.position.x, rooms[x, y].transform.position.y + 55);
                }
            }
        }

        rooms[(int)currentRoom.x, (int)currentRoom.y].SetActive(false);
        previousRoom = new Vector2(currentRoom.x, currentRoom.y);
        currentRoom = new Vector2(currentRoom.x, currentRoom.y-1);

        foreach (Transform child in rooms[(int)currentRoom.x, (int)currentRoom.y].transform)
        {
            if (child.CompareTag("Up"))
            {
                player.transform.position = new Vector2(child.transform.position.x, child.transform.position.y - 3);
                cam.transform.position = player.transform.position;
                break;
            }
        }

        grassTilemap.transform.position = new Vector2(grassTilemap.transform.position.x, grassTilemap.transform.position.y + 55);
        wallTilemap.transform.position = new Vector2(wallTilemap.transform.position.x, wallTilemap.transform.position.y + 55);
        ledgeTilemap.transform.position = new Vector2(ledgeTilemap.transform.position.x, ledgeTilemap.transform.position.y + 55);

        mapRooms[(int)currentRoom.x, (int)currentRoom.y].transform.parent.GetChild(0).transform.position = mapRooms[(int)currentRoom.x, (int)currentRoom.y].transform.position;


        rooms[(int)currentRoom.x, (int)currentRoom.y].SetActive(true);

        CheckRoomTypeForAudio((int)currentRoom.x, (int)currentRoom.y, previousRoom);

        Debug.Log(currentRoom);
    }

    public void MoveRoomUp()
    {


        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (rooms[x,y] != null) { 
                    rooms[x, y].transform.position = new Vector2(rooms[x, y].transform.position.x, rooms[x, y].transform.position.y - 55);
                   
                }
            }
        }

        rooms[(int)currentRoom.x, (int)currentRoom.y].SetActive(false);
        previousRoom = new Vector2(currentRoom.x, currentRoom.y);
        currentRoom = new Vector2(currentRoom.x, currentRoom.y + 1);

        foreach (Transform child in rooms[(int)currentRoom.x, (int)currentRoom.y].transform)
        {
            if (child.CompareTag("Down"))
            {
                player.transform.position = new Vector2(child.transform.position.x , child.transform.position.y + 3);
                cam.transform.position = player.transform.position;
                break;
            }
        }
        //player.transform.position = rooms[(int)currentRoom.x, (int)currentRoom.y].transform.GetChild();
        grassTilemap.transform.position = new Vector2(grassTilemap.transform.position.x, grassTilemap.transform.position.y - 55);
        wallTilemap.transform.position = new Vector2(wallTilemap.transform.position.x, wallTilemap.transform.position.y - 55);
        ledgeTilemap.transform.position = new Vector2(ledgeTilemap.transform.position.x, ledgeTilemap.transform.position.y - 55);

        mapRooms[(int)currentRoom.x, (int)currentRoom.y].transform.parent.GetChild(0).transform.position = mapRooms[(int)currentRoom.x, (int)currentRoom.y].transform.position;

        rooms[(int)currentRoom.x, (int)currentRoom.y].SetActive(true);

        CheckRoomTypeForAudio((int)currentRoom.x, (int)currentRoom.y, previousRoom);

        Debug.Log(currentRoom);
    }

    public void MoveRoomRight()
    {

        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (rooms[x, y] != null) { 
                    rooms[x, y].transform.position = new Vector2(rooms[x, y].transform.position.x - 80, rooms[x, y].transform.position.y);
                    
                }
            }
        }

        rooms[(int)currentRoom.x, (int)currentRoom.y].SetActive(false);
        previousRoom = new Vector2(currentRoom.x, currentRoom.y);
        currentRoom = new Vector2(currentRoom.x+1, currentRoom.y);

        foreach (Transform child in rooms[(int)currentRoom.x, (int)currentRoom.y].transform)
        {
            if (child.CompareTag("Left"))
            {
                player.transform.position = new Vector2(child.transform.position.x+3, child.transform.position.y);
                cam.transform.position = player.transform.position;
                break;
            }
        }

        grassTilemap.transform.position = new Vector2(grassTilemap.transform.position.x - 80, grassTilemap.transform.position.y);
        wallTilemap.transform.position = new Vector2(wallTilemap.transform.position.x - 80, wallTilemap.transform.position.y);
        ledgeTilemap.transform.position = new Vector2(ledgeTilemap.transform.position.x - 80, ledgeTilemap.transform.position.y);

        mapRooms[(int)currentRoom.x, (int)currentRoom.y].transform.parent.GetChild(0).transform.position = mapRooms[(int)currentRoom.x, (int)currentRoom.y].transform.position;

        rooms[(int)currentRoom.x, (int)currentRoom.y].SetActive(true);

        CheckRoomTypeForAudio((int)currentRoom.x, (int)currentRoom.y, previousRoom);

        Debug.Log(currentRoom);

    }

    public void MoveRoomLeft()
    {

        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (rooms[x, y] != null) { 
                    rooms[x, y].transform.position = new Vector2(rooms[x, y].transform.position.x + 80, rooms[x, y].transform.position.y);
                   
                } 
            }
        }

        rooms[(int)currentRoom.x, (int)currentRoom.y].SetActive(false);
        previousRoom = new Vector2(currentRoom.x, currentRoom.y);
        currentRoom = new Vector2(currentRoom.x - 1, currentRoom.y);

        foreach (Transform child in rooms[(int)currentRoom.x, (int)currentRoom.y].transform)
        {
            if (child.CompareTag("Right"))
            {
                player.transform.position = new Vector2(child.transform.position.x - 3, child.transform.position.y);
                cam.transform.position = player.transform.position;
                break;
            }
        }

        grassTilemap.transform.position = new Vector2(grassTilemap.transform.position.x + 80, grassTilemap.transform.position.y);
        wallTilemap.transform.position = new Vector2(wallTilemap.transform.position.x + 80, wallTilemap.transform.position.y);
        ledgeTilemap.transform.position = new Vector2(ledgeTilemap.transform.position.x + 80, ledgeTilemap.transform.position.y);

        mapRooms[(int)currentRoom.x, (int)currentRoom.y].transform.parent.GetChild(0).transform.position = mapRooms[(int)currentRoom.x, (int)currentRoom.y].transform.position;


        rooms[(int)currentRoom.x, (int)currentRoom.y].SetActive(true);

        CheckRoomTypeForAudio((int)currentRoom.x, (int)currentRoom.y, previousRoom);

        Debug.Log(currentRoom);

    }

    public void CheckRoomTypeForAudio(int x, int y, Vector2 previousRoom)
    {
        if (rooms[x, y].GetComponent<RoomInstance>().type == 1) //home
        {
            if (rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 2) //if previous room was boss 
            {
                SoundManager.StopSoundtrack(soundtrackAudioSource3); //stop boss track
            }
            else if (rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 0 ||
                rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 3 ||
                rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 4) //exploration rooms
            {
                SoundManager.StopSoundtrack(soundtrackAudioSource2); //stop explore track
            }
            //play the home track
            soundtrackAudioSource = SoundManager.PlaySoundTrack(SoundManager.Sound.homeTrack);

        } else if (rooms[x, y].GetComponent<RoomInstance>().type == 0 || rooms[x, y].GetComponent<RoomInstance>().type == 3 || rooms[x, y].GetComponent<RoomInstance>().type == 4) //exploration rooms
        {
            if (rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 1) //if previous rooms was home room 
            {
                SoundManager.StopSoundtrack(soundtrackAudioSource);
                soundtrackAudioSource2 = SoundManager.PlaySoundTrack2(SoundManager.Sound.exploreTrack);

            }
            else if (rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 2) //if previous room was boss room
            {
                SoundManager.StopSoundtrack(soundtrackAudioSource3);
                soundtrackAudioSource2 = SoundManager.PlaySoundTrack2(SoundManager.Sound.exploreTrack);
            }
            else //if previous room was exploration room
            {
                return;
                //do nothing
            }

        } else if (rooms[x, y].GetComponent<RoomInstance>().type == 2) //boss room
        {

            //if previous rooms was home room 
            if (rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 1)
            {
                SoundManager.StopSoundtrack(soundtrackAudioSource); //stop home track
            }
            else if (rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 0 ||
                rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 3 ||
                rooms[(int)previousRoom.x, (int)previousRoom.y].GetComponent<RoomInstance>().type == 4) //exploration rooms
            {
                SoundManager.StopSoundtrack(soundtrackAudioSource2); //stop explore track
            }

            //play the boss track
            soundtrackAudioSource3 = SoundManager.PlaySoundTrack3(SoundManager.Sound.bossTrack);

        }
    }

    //public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    //{
    //    float currentTime = 0;
    //    float start = audioSource.volume;

    //    while (currentTime < duration)
    //    {
    //        currentTime += Time.deltaTime;
    //        audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
    //        yield return null;
    //    }
    //    yield break;
    //}


    public Vector2 GetCurrentRoom()
    {
        return currentRoom;
    }

    public GameObject GetCurrentRoomInstance()
    {
        return rooms[(int)currentRoom.x, (int)currentRoom.y];
    }

    public GameObject GetStartingRoomInstance()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (rooms[x, y] != null)
                {
                    if (rooms[x,y].GetComponent<RoomInstance>().type == 1)
                    {
                        return rooms[x, y];
                    }

                }
            }
        }
        return null;
    }

    public void GiveRoomInstancesArray(GameObject[,] ri)
    {
        rooms = ri;
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (rooms[x, y] != null)
                {
                    if (x == currentRoom.x && y == currentRoom.y)
                    {
                        rooms[x, y].SetActive(true);
                    } else
                    {
                        rooms[x, y].SetActive(false);
                    }
                }
            }
        }
    }

    public void GiveMapRoomsArray(GameObject[,] mr)
    {
        mapRooms = mr;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
	Vector2 worldSize = new Vector2(10, 10);
	Room[,] rooms;

	GameObject[,] mapRooms;

	List<Vector2> takenPositions = new List<Vector2>();
	int gridSizeX, gridSizeY, numberOfRooms = 10;
	public GameObject roomWhiteObj;

	public Transform mapRoot;

	void Start()
	{
		if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
		{ // make sure we dont try to make more rooms than can fit in our grid
			numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
		}
		gridSizeX = Mathf.RoundToInt(worldSize.x); //note: these are half-extents
		gridSizeY = Mathf.RoundToInt(worldSize.y);
		CreateRooms(); //lays out the actual map
		SetRoomDoors(); //assigns the doors where rooms would connect
		DrawMap(); //instantiates objects to make up a map
		GetComponent<SheetAssigner>().Assign(rooms); //passes room info to another script which handles generatating the level geometry
	}
	void CreateRooms()
	{
		//setup
		rooms = new Room[gridSizeX * 2, gridSizeY * 2];

		rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1); //SETS THE FIRST ROOM W/ TYPE 1

		mapRooms = new GameObject[gridSizeX * 2, gridSizeY * 2];

		//Debug.Log(gridSizeX + " " + gridSizeY);

		//Debug.Log(rooms[gridSizeX, gridSizeY].type);

		takenPositions.Insert(0, Vector2.zero);
		Vector2 checkPos = Vector2.zero;
		//magic numbers
		float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
		//add rooms
		for (int i = 0; i < numberOfRooms - 1; i++)
		{
			float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
			randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
			//grab new position
			checkPos = NewPosition();
			//test new position
			if (NumberOfNeighbours(checkPos, takenPositions) > 1 && Random.value > randomCompare)
			{
				int iterations = 0;
				do
				{
					checkPos = SelectiveNewPosition();
					iterations++;
				} while (NumberOfNeighbours(checkPos, takenPositions) > 1 && iterations < 100);
				if (iterations >= 50)
					print("error: could not create with fewer neighbors than : " + NumberOfNeighbours(checkPos, takenPositions));
			}

			//finalize position
			// CHOOSE ROOM TYPES
			if (i != numberOfRooms - 2) {
				if (i != 5) {

					if (i % 2 == 0) { 
						rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0); //default trees
					} else { 
						rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 4); //default rocks
					}

				}
				else 
                { //room number 5
					rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 3); //wasp
				}
			} else { //room number 8
				rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 2); //boss 
			}
			takenPositions.Insert(0, checkPos);
		}

	}

	Vector2 NewPosition()
	{
		int x = 0, y = 0;
		Vector2 checkingPos = Vector2.zero;
		do
		{
			int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
			x = (int)takenPositions[index].x;//capture its x, y position
			y = (int)takenPositions[index].y;
			bool UpDown = (Random.value < 0.5f);//randomly pick whether to look on hor or vert axis
			bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
			if (UpDown)
			{ //find the position bnased on the above bools
				if (positive)
				{
					//y += 1;
					y -= 1;

				}
				else
				{
					y -= 1;
				}
			}
			else
			{
				if (positive)
				{
					x += 1;
				}
				else
				{
					//x -= 1;
					x+=1;
				}
			}
			checkingPos = new Vector2(x, y);
		} while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
		return checkingPos;
	}

	Vector2 SelectiveNewPosition()
	{ // method differs from the above in the two commented ways
		int index = 0, inc = 0;
		int x = 0, y = 0;
		Vector2 checkingPos = Vector2.zero;
		do
		{
			inc = 0;
			do
			{
				//instead of getting a room to find an adject empty space, we start with one that only 
				//as one neighbor. This will make it more likely that it returns a room that branches out
				index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
				inc++;
			} while (NumberOfNeighbours(takenPositions[index], takenPositions) > 1 && inc < 100);
			x = (int)takenPositions[index].x;
			y = (int)takenPositions[index].y;


			bool UpDown = (Random.value < 0.5f);
			bool positive = (Random.value < 0.5f);
			if (UpDown)
			{
				if (positive)
				{
					//y += 1;
					y -= 1;
				}
				else
				{
					y -= 1;
				}
			}
			else
			{
				if (positive)
				{
					x += 1;
				}
				else
				{
					//x -= 1;
					x+=1;
				}
			}
			checkingPos = new Vector2(x, y);
		} while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
		if (inc >= 100)
		{ // break loop if it takes too long: this loop isnt guranteed to find solution, which is fine for this
			print("Error: could not find position with only one neighbour");
		}
		return checkingPos;
	}

	int NumberOfNeighbours(Vector2 checkingPos, List<Vector2> usedPositions)
	{
		int ret = 0; // start at zero, add 1 for each side there is already a room
		if (usedPositions.Contains(checkingPos + Vector2.right))
		{ //using Vector.[direction] as short hands, for simplicity
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.left))
		{
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.up))
		{
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.down))
		{
			ret++;
		}
		return ret;
	}
	void DrawMap()
	{
		//foreach (Room room in rooms)
		//{
		//	if (room == null)
		//	{
		//		continue; //skip where there is no room
		//	}
		//	Vector2 drawPos = room.gridPos;
		//	drawPos.x *= 1f;//aspect ratio of map sprite
		//	drawPos.y *= 0.5f;
		//	//create map obj and assign its variables

		//	//MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();
		//	MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();

		//	mapper.type = room.type;
		//	mapper.up = room.doorTop;
		//	mapper.down = room.doorBot;
		//	mapper.right = room.doorRight;
		//	mapper.left = room.doorLeft;
		//	mapper.gameObject.transform.parent = mapRoot;
		//}

		for (int i = 0; i < 20; i++)
        {
			for (int j = 0; j < 20; j++)
            {
				if (rooms[i,j] == null)
				{
					continue; //skip where there is no room
				}

				Vector2 drawPos = rooms[i, j].gridPos;
				drawPos.x *= 0.5f;//aspect ratio of map sprite
				drawPos.y *= 0.5f;
				//create map obj and assign its variables

				//MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();

				GameObject mapperGameObject = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity);
				MapSpriteSelector mapper = mapperGameObject.GetComponent<MapSpriteSelector>();

				mapRooms[i,j] = mapperGameObject;

				mapper.type = rooms[i, j].type;
				mapper.up = rooms[i, j].doorTop;
				mapper.down = rooms[i, j].doorBot;
				mapper.right = rooms[i, j].doorRight;
				mapper.left = rooms[i, j].doorLeft;

				mapper.gameObject.transform.parent = mapRoot;

				Vector3 pos = mapper.gameObject.transform.position;
				pos.z += 2f;
				mapper.transform.position = pos;

			}
        }
		mapRoot.transform.GetChild(0).gameObject.SetActive(true);
		//mapRoot.transform.GetChild(0).position = mapRooms[3,3].transform.position;
		RoomManager.instance.GiveMapRoomsArray(mapRooms);

	}
	void SetRoomDoors()
	{
		for (int x = 0; x < ((gridSizeX * 2)); x++)
		{
			for (int y = 0; y < ((gridSizeY * 2)); y++)
			{
				if (rooms[x, y] == null)
				{
					continue;
				} 
               
				Vector2 gridPosition = new Vector2(x, y);
				if (y - 1 < 0)
				{ //check above
					rooms[x, y].doorBot = false;
				}
				else
				{
					rooms[x, y].doorBot = (rooms[x, y - 1] != null);
				}
				if (y + 1 >= gridSizeY * 2)
				{ //check bellow
					rooms[x, y].doorTop = false;
				}
				else
				{
					rooms[x, y].doorTop = (rooms[x, y + 1] != null);
				}
				if (x - 1 < 0)
				{ //check left
					rooms[x, y].doorLeft = false;
				}
				else
				{
					rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
				}
				if (x + 1 >= gridSizeX * 2)
				{ //check right
					rooms[x, y].doorRight = false;
				}
				else
				{
					rooms[x, y].doorRight = (rooms[x + 1, y] != null);
				}

				if (rooms[x, y].type == 1)
				{
					//Debug.Log(x + " " + y + " *");
				}
				else
				{
					//Debug.Log(x + " " + y);
				}

				//RoomManager.instance.AddRoomToArray(rooms[x,y], x, y);

			}
		}
	}

	public Room[,] GetRooms()
    {
		return rooms;
    }

	public GameObject[,] GetMapRooms()
    {
		return mapRooms;
    }

}
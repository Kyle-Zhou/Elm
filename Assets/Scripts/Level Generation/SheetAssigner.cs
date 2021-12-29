using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SheetAssigner : MonoBehaviour
{
	[SerializeField]
	Texture2D[] sheetsNormal;
	[SerializeField]
	GameObject RoomObj;
	public Vector2 roomDimensions, gutterSize;
	public Tilemap tilemap, walls, ledges;
	public TileBase tile, wall;
	public TileBase[] ledgeTiles;
	public LayerMask entranceLayer;

	private GameObject[,] roomInstances;

	private int count;

	public void Assign(Room[,] rooms)
	{
		roomInstances = new GameObject[20, 20];
		count = 0;

		for (int x = 0; x < 20; x++)
		{
			for (int y = 0; y < 20; y++)
			{
				if (rooms[x, y] == null)
				{
					continue;
				}

				count++;

				int index = 0;
				//pick a random index for the array
				if (rooms[x, y].type == 0 || rooms[x, y].type == 4)
				{
					index = Mathf.RoundToInt(Random.value * (sheetsNormal.Length - 2));
				}
				else
                {
					index = 10;
                }
				//find position to place room
				Vector3 pos = new Vector3(rooms[x, y].gridPos.x * (roomDimensions.x + gutterSize.x), rooms[x, y].gridPos.y * (roomDimensions.y + gutterSize.y), 0);
				GameObject roomGameObject = Instantiate(RoomObj, pos, Quaternion.identity);
				RoomInstance myRoom = roomGameObject.GetComponent<RoomInstance>();

				//Debug.Log((int)room.gridPos.x + " " + (int)room.gridPos.y);

				myRoom.Setup(sheetsNormal[index], rooms[x, y].gridPos, rooms[x, y].type, rooms[x, y].doorTop, rooms[x, y].doorBot, rooms[x, y].doorLeft, rooms[x, y].doorRight);

				myRoom.name += count;

				roomInstances[x,y] = roomGameObject;
				Debug.Log(x + " " + y);

			}
		}

		RoomManager.instance.GiveRoomInstancesArray(roomInstances);

		foreach (Vector3Int pos in RoomInstance.positions)
		{
			tilemap.SetTile(pos, tile);
		}

		foreach (Vector3Int pos in RoomInstance.wallPositions)
		{
			walls.SetTile(pos, wall);
		}

		foreach (Vector3Int pos in RoomInstance.wallPositions)
        {
			Vector3Int north = new Vector3Int(pos.x, pos.y + 1, pos.z);
			Vector3Int east = new Vector3Int(pos.x + 1, pos.y, pos.z);
			Vector3Int south = new Vector3Int(pos.x, pos.y - 1, pos.z);
			Vector3Int west = new Vector3Int(pos.x - 1, pos.y, pos.z);

			if (walls.HasTile(north) && walls.HasTile(east) && !walls.HasTile(south) && !walls.HasTile(west))
            {
				ledges.SetTile(pos, ledgeTiles[0]);
			}
			else if (walls.HasTile(north) && walls.HasTile(east) && !walls.HasTile(south) && walls.HasTile(west))
            {
				ledges.SetTile(pos, ledgeTiles[1]);
			}
			else if (walls.HasTile(north) && !walls.HasTile(east) && !walls.HasTile(south) && walls.HasTile(west))
            {
				ledges.SetTile(pos, ledgeTiles[2]);
			}
        }

		foreach (Vector3 pos in RoomInstance.entrancePositions)
        {
			Vector3Int cell = ledges.WorldToCell(pos);
			Vector3Int neighbour = new Vector3Int(cell.x - 1, cell.y, cell.z);

			if (ledges.HasTile(cell))
            {
				ledges.SetTile(cell, null);
				ledges.SetTile(neighbour, null);
				tilemap.SetTile(cell, tile);
				tilemap.SetTile(neighbour, tile);
			}
        }
	}

	//give RoomManager 2d array of room instances

	public void GiveRM(Room[,] rooms)
    {
		for (int x = 0; x < 6; x++)
        {
			for (int y = 0; y < 6; y++)
            {
				if (rooms[x,y] != null)
                {
					//RoomManager.instance.AddRoomToArray(rooms[x,y], x, y);

				}
			}
        }
    }

}
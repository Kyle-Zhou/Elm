using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class RoomInstance : MonoBehaviour
{
	public Texture2D tex;
	[HideInInspector]
	public Vector2 gridPos;
	public int type; // 0: normal, 1: enter, 2: boss, 3: bee nest??
	[HideInInspector]
	public bool doorTop, doorBot, doorLeft, doorRight;
	[SerializeField]
	GameObject doorU, doorD, doorL, doorR;
	[SerializeField]
	PixelInterpreter[] mappings;
	float tileSize = 1;
	public Tilemap tilemap, walls;
	public TileBase tile, wall;
	public static List<Vector3Int> positions = new List<Vector3Int>();
	public static List<Vector3Int> wallPositions = new List<Vector3Int>();
	public static List<Vector3> entrancePositions = new List<Vector3>();

	public List<GameObject> objects;
	public List<GameObject> resources;
	public List<GameObject> crystals;
	public List<GameObject> mushrooms;
	public List<GameObject> foliage;

	public List<Mob> mobs;
	public GameObject waspNest;

	//turn into lists later
	public GameObject totem1;
	public GameObject totemMenu1;
	//------------------------------


	public LayerMask entranceLayer, obstacleLayer, torchLayer, topDecorationLayer, outlineLayer, grassLayer;

	//int tileSize = 1;

	Vector2 roomSizeInTiles = new Vector2(40, 60);


	//public Tilemap tilemap;
	//public TileBase tile;

	public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight)
	{
		tex = _tex;
		gridPos = _gridPos;
		type = _type;
		doorTop = _doorTop;
		doorBot = _doorBot;
		doorLeft = _doorLeft;
		doorRight = _doorRight;
		MakeDoors();
		GenerateRoomTiles();
	}
	void MakeDoors()
	{
		//top door, get position then spawn
		Vector3 spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
		PlaceDoor(spawnPos, doorTop, doorU, 2);
		//bottom door
		spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
		PlaceDoor(spawnPos, doorBot, doorD, 3);
		//right door
		spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
		PlaceDoor(spawnPos, doorRight, doorR, 4);
		//left door
		spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
		PlaceDoor(spawnPos, doorLeft, doorL, 5);
	}
	void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn, int index)
	{
		// check whether its a door or wall, then spawn
		if (door)
		{
			mappings[index].tile = tile;
			mappings[index + 4].tile = tile;
			mappings[index + 4].entrance = doorSpawn;
		}
		else
		{
			mappings[index].tile = wall;
			mappings[index + 4].tile = wall;
		}
	}
	void GenerateRoomTiles()
	{
		//loop through every pixel of the texture
		for (int x = 0; x < tex.width; x++)
		{
			for (int y = 0; y < tex.height; y++)
			{
				GenerateTile(x, y);
			}
		}

		SpawnEntities();
	}

	public void ClearStaticLists()
    {
		positions.Clear();
		wallPositions.Clear();
		entrancePositions.Clear();
    }

	void GenerateTile(int x, int y)
	{
		Color pixelColour = tex.GetPixel(x, y);
		//skip clear spaces in texture
		if (pixelColour.a == 0)
		{
			return;
		}
		//find the color to math the pixel
		foreach (PixelInterpreter mapping in mappings)
		{
			if (mapping.colour.Equals(pixelColour))
			{
				Vector3 spawnPos = positionFromTileGrid(x, y);

				if (pixelColour.Equals(new Color(128, 128, 128)) || mapping.tile == wall)
				{
					walls.SetTile(walls.WorldToCell(spawnPos), mapping.tile);
					wallPositions.Add(walls.WorldToCell(spawnPos));
				}
				else if (mapping.tile != null && mapping.entrance == null)
				{
					tilemap.SetTile(tilemap.WorldToCell(spawnPos), mapping.tile);
				}
				else if (mapping.tile != null && mapping.entrance != null)
				{
					tilemap.SetTile(tilemap.WorldToCell(spawnPos), mapping.tile);
					spawnPos = tilemap.WorldToCell(spawnPos);

					switch (mapping.entrance.tag)
					{
						case "Up":
							spawnPos.x += 1f;
							spawnPos.y += 1.5f;
							entrancePositions.Add(spawnPos);
							break;
						case "Down":
							spawnPos.x += 1f;
							spawnPos.y += 0.5f;
							break;
						case "Right":
							spawnPos.x += 0.5f;
							spawnPos.y += 1f;
							break;
						case "Left":
							spawnPos.x += 0.5f;
							spawnPos.y += 1f;
							break;
					}

					GameObject e = Instantiate(mapping.entrance, spawnPos, Quaternion.identity);
					e.transform.SetParent(gameObject.transform);
				}
				//tilemap.SetTile(spawnPos, tile);
				//tilemap.SetTile(new Vector3Int(1,1,1), tile);
			}
			else
			{
				//forgot to remove the old print for the tutorial lol so I'll leave it here too
				//print(mapping.color + ", " + pixelColor);
			}
		}
	}


	Vector3 positionFromTileGrid(int x, int y)
	{
		Vector3 ret;
		//find difference between the corner of the texture and the center of this object
		Vector3 offset = new Vector3((-roomSizeInTiles.x + 1) * tileSize, (roomSizeInTiles.y / 4) * tileSize - (tileSize / 4), 0);
		//find scaled up position at the offset
		ret = new Vector3(tileSize * (float)x, -tileSize * (float)y, 0) + offset + transform.position;
		return ret;
	}

	void SpawnEntities()
	{
		List<Vector3Int> spawnpoints = new List<Vector3Int>();
		List<Vector3Int> totemTiles = new List<Vector3Int>();
		List<Vector3Int> wallTiles = new List<Vector3Int>();

		foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
		{

			if (tilemap.HasTile(pos))
			{
				positions.Add(pos);
				spawnpoints.Add(pos);
				totemTiles.Add(pos);
			}
			else if (walls.HasTile(pos))
			{
				totemTiles.Add(pos);
				wallTiles.Add(pos);
			}
		}



		switch (type)
		{
			case 0: //default trees
					//SpawnCrystals(spawnpoints);

				SpawnBrownMushrooms(spawnpoints);
				SpawnWheat(spawnpoints);
				SpawnCarrots(spawnpoints);
				//SpawnResources(spawnpoints);
				SpawnPlants(spawnpoints);

				SpawnFoliage(spawnpoints);


				SpawnMobs(spawnpoints);
				break;
			case 1: //home room
				break;
			case 2: //boss room
				SpawnBoss();
				break;
			case 3: // wasp room //NVM no wasp nest (copy pasted case 0)
				SpawnBrownMushrooms(spawnpoints);
				SpawnWheat(spawnpoints);
				SpawnCarrots(spawnpoints);
				//SpawnResources(spawnpoints);
				SpawnPlants(spawnpoints);

				SpawnFoliage(spawnpoints);


				SpawnMobs(spawnpoints); break;
			case 4: //default rocks
				SpawnCrystals(spawnpoints);
				SpawnMushrooms(spawnpoints);
				SpawnRocks(spawnpoints);
				break;
		}

        foreach (Vector3Int pos in walls.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                tilemap.SetTile(pos, null);
            }
            else if (walls.HasTile(pos))
            {
                walls.SetTile(pos, null);
            }
        }
    }


	public void SpawnTrees()
    {
		int randomIncrement = Random.Range(14, 27);

		for (int i = 0; i < wallPositions.Count; i += randomIncrement)
		{
			randomIncrement = Random.Range(14, 27);

			Vector3 spawnpoint = tilemap.CellToWorld(wallPositions[i]);
			spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z);

			Collider2D[] trees = Physics2D.OverlapCircleAll(spawnpoint, 3.5f, topDecorationLayer);
			Collider2D[] outlineTiles = Physics2D.OverlapCircleAll(spawnpoint, 10.5f, outlineLayer);
			Collider2D[] grassTiles = Physics2D.OverlapCircleAll(spawnpoint, 10.5f, grassLayer);

			if (trees.Length == 0 && grassTiles.Length == 0 && outlineTiles.Length == 0) { 
				GameObject t = Instantiate(objects[1], spawnpoint, Quaternion.identity);
				t.transform.SetParent(gameObject.transform);
			}

		}
	}

	public void SpawnTotem(List<Vector3Int> totemTiles)
    {
		GameObject t = Instantiate(totem1, tilemap.CellToWorld(totemTiles[(totemTiles.Count / 2) + 30]), Quaternion.identity);
		t.transform.SetParent(gameObject.transform);
		t.GetComponent<Totem>().GetMenu(totemMenu1);
		totemMenu1.GetComponent<TotemMenu>().GetTotem(t);
	}

	public void SpawnBoss()
    {
		GameObject[] boss = GameObject.FindGameObjectsWithTag("Gatekeeper");
		boss[0].transform.SetParent(gameObject.transform);
		boss[0].transform.position = new Vector2(transform.position.x - 9, transform.position.y - 3.5f);
	}

	public void SpawnWaspNest(List<Vector3Int> totemTiles)
    {

		Vector3Int spawnPos = totemTiles[0];
		Vector3Int newSpawnPos;
		spawnPos.x += 28;
		spawnPos.y += 18;

		newSpawnPos = spawnPos;

		GameObject g = Instantiate(waspNest, tilemap.CellToWorld(newSpawnPos), Quaternion.identity);
		//GameObject g = Instantiate(waspNest, tilemap.CellToWorld(totemTiles[1200]), Quaternion.identity);
		g.transform.SetParent(gameObject.transform);
		//Debug.Log((totemTiles.Count / 2) + 30);
		//sDebug.Log(totemTiles.Count);
		//Debug.Log(totemTiles[1200]);
	}

	public void SpawnCrystals(List<Vector3Int> spawnpoints)
	{
		//crystals
		for (int i = 0; i < 100; i++)
		{
			if ((int)Random.Range(0, 8) == 1)
			{
				Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);
				spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z); ////////////////////////THIS -6f IS WHY IT'S CURSED

				Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnpoint, 3.5f, obstacleLayer);
				Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, entranceLayer);


				if (colliders.Length == 0 && entrances.Length == 0)
				{
					int x = Random.Range(0, crystals.Count);
					GameObject g = Instantiate(crystals[x], spawnpoint, Quaternion.identity);
					g.transform.SetParent(gameObject.transform);
				}
			}
		}

	}

	public void SpawnMushrooms(List<Vector3Int> spawnpoints)
	{
		//shrooms
		for (int i = 0; i < 100; i++)
		{
			if ((int)Random.Range(0, 8) == 1)
			{
				Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);
				spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z);

				Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, obstacleLayer);
				Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, entranceLayer);


				if (colliders.Length == 0 && entrances.Length == 0)
				{
					int x = Random.Range(0, mushrooms.Count);
					GameObject g = Instantiate(mushrooms[x], spawnpoint, Quaternion.identity);
					g.transform.SetParent(gameObject.transform);
				}
			}
		}
	}

	public void SpawnBrownMushrooms(List<Vector3Int> spawnpoints)
	{
		//shrooms
		for (int i = 0; i < 100; i++)
		{
			if ((int)Random.Range(0, 20) == 1)
			{
				Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);
				spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z);

				Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, obstacleLayer);
				Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, entranceLayer);


				if (colliders.Length == 0 && entrances.Length == 0)
				{
					int x = 2; //brown
					GameObject g = Instantiate(mushrooms[x], spawnpoint, Quaternion.identity);
					g.transform.SetParent(gameObject.transform);
				}
			}
		}
	}

	public void SpawnFoliage(List<Vector3Int> spawnpoints)
	{
		//foliage
		int[] options = { 1, 0, -1 };
		for (int i = 0; i < 3; i++)
		{
			Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);

			int wheatCount = (int)Random.Range(8, 14);
			for (int j = 0; j < wheatCount; j++)
			{
				int randomDifference1 = options[Random.Range(0, 3)];
				int randomDifference2 = options[Random.Range(0, 3)];

				spawnpoint = new Vector3(spawnpoint.x + 0.5f + randomDifference1, spawnpoint.y + 0.5f + randomDifference2, spawnpoint.z);
				Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 0.5f, entranceLayer);

				if (!walls.HasTile(walls.WorldToCell(spawnpoint)) && entrances.Length == 0)
				{
					int random = Random.Range(0,6);
					if (random > 3) random = 0;
                  
					GameObject g = Instantiate(foliage[random], spawnpoint, Quaternion.identity);
					g.transform.SetParent(gameObject.transform);
				}
			}
		}
	}

	public void SpawnWheat(List <Vector3Int> spawnpoints)
    {
		//wheat
		int[] options = { 1, 0, -1 };
		for (int i = 0; i < 3; i++)
		{
			Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);

			int wheatCount = (int)Random.Range(6, 9);
			for (int j = 0; j < wheatCount; j++)
			{
				int randomDifference1 = options[Random.Range(0, 3)];
				int randomDifference2 = options[Random.Range(0, 3)];

				spawnpoint = new Vector3(spawnpoint.x + 0.5f + randomDifference1, spawnpoint.y + 0.5f + randomDifference2, spawnpoint.z);
				Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 0.5f, entranceLayer);

				if (!walls.HasTile(walls.WorldToCell(spawnpoint)) && entrances.Length == 0) { 
					GameObject g = Instantiate(resources[4], spawnpoint, Quaternion.identity);
					g.transform.SetParent(gameObject.transform);
				}
			}
		}
	}
	public void SpawnCarrots(List<Vector3Int> spawnpoints)
	{
		//carrots
		int[] options = { 1, 0, -1 };
		for (int i = 0; i < 3; i++)
		{
			Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);

			int carrotCount = (int)Random.Range(3, 4);
			for (int j = 0; j < carrotCount; j++)
			{
				int randomDifference1 = options[Random.Range(0, 3)];
				int randomDifference2 = options[Random.Range(0, 3)];

				spawnpoint = new Vector3(spawnpoint.x + 0.5f + randomDifference1, spawnpoint.y + 0.5f + randomDifference2, spawnpoint.z);
				Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 0.5f, entranceLayer);

				if (!walls.HasTile(walls.WorldToCell(spawnpoint)) && entrances.Length == 0) { 
					GameObject g = Instantiate(resources[3], spawnpoint, Quaternion.identity);
					g.transform.SetParent(gameObject.transform);
				}
			}
		}
	}


	public void SpawnTorches(List<Vector3Int> spawnpoints)
	{
		//torches
		for (int i = 0; i < 6; i++)
		{
			Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);
			spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z);

			Collider2D[] torches = Physics2D.OverlapCircleAll(spawnpoint, 3.5f, torchLayer);
			Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, entranceLayer);
			Collider2D[] obstacles = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, obstacleLayer);


			if (torches.Length == 0 && entrances.Length == 0 && obstacles.Length == 0)
			{
				GameObject torch = Instantiate(objects[0], spawnpoint, Quaternion.identity);
				DayNightCycle.instance.AddLightSource(torch);
				//sun.GetComponent<DayNightCycle>().AddLightSource(torch);
				torch.transform.SetParent(gameObject.transform);
			}
		}
	}
	//public void SpawnResources(List<Vector3Int> spawnpoints)
	//{
	//	//resources
	//	for (int i = 0; i < 100; i++)
	//	{
	//		if ((int)Random.Range(0, 8) == 1)
	//		{
	//			Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);
	//			spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z);

	//			Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, obstacleLayer);
	//			Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, entranceLayer);


	//			if (colliders.Length == 0 && entrances.Length == 0)
	//			{
	//				///int resource = (int)Random.Range(0, resources.Count);
	//				//if (resource == 4 || resource == 3) resource = 2;
	//				int resource = (int)Random.Range(0, resources.Count - 2);
	//				GameObject g = Instantiate(resources[resource], spawnpoint, Quaternion.identity);
	//				g.transform.SetParent(gameObject.transform);
	//			}
	//		}
	//	}
	//}

	public void SpawnPlants(List<Vector3Int> spawnpoints)
	{
		//resources
		for (int i = 0; i < 100; i++)
		{
			if ((int)Random.Range(0, 8) == 1)
			{
				Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);
				spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z);

				Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, obstacleLayer);
				Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, entranceLayer);


				if (colliders.Length == 0 && entrances.Length == 0)
				{
					int resource = (int)Random.Range(1, resources.Count - 2); //everything but rocks
					GameObject g = Instantiate(resources[resource], spawnpoint, Quaternion.identity);
					g.transform.SetParent(gameObject.transform);
				}
			}
		}
	}
	public void SpawnRocks(List<Vector3Int> spawnpoints)
	{
		//resources
		for (int i = 0; i < 100; i++)
		{
			if ((int)Random.Range(0, 8) == 1)
			{
				Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);
				spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z);

				Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, obstacleLayer);
				Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, entranceLayer);


				if (colliders.Length == 0 && entrances.Length == 0)
				{
					///int resource = (int)Random.Range(0, resources.Count);
					//if (resource == 4 || resource == 3) resource = 2;
					int resource = 0; //rocks
					GameObject g = Instantiate(resources[resource], spawnpoint, Quaternion.identity);
					g.transform.SetParent(gameObject.transform);
				}
			}
		}
	}

	public void SpawnMobs(List<Vector3Int> spawnpoints)
	{
		//mobs
		for (int i = 0; i < 6; i++)
		{
			Vector3 spawnpoint = tilemap.CellToWorld(spawnpoints[Random.Range(0, spawnpoints.Count)]);
			spawnpoint = new Vector3(spawnpoint.x + 0.5f, spawnpoint.y + 0.5f, spawnpoint.z);

			Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnpoint, 0.5f, obstacleLayer);
			Collider2D[] entrances = Physics2D.OverlapCircleAll(spawnpoint, 2.5f, entranceLayer);

			if (colliders.Length == 0 && entrances.Length == 0)
			{
				int mob = (int)Random.Range(0, mobs.Count);
				Mob m = Instantiate(mobs[mob], spawnpoint, Quaternion.identity);
				m.transform.SetParent(gameObject.transform);
			}
		}
	}


}

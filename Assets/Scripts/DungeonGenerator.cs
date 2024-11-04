using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public int dungeonWidth = 50;
    public int dungeonHeight = 50;
    public int roomCount = 5;
    public int roomWidthMin = 4, roomHeightMin = 4, roomWidthMax = 8, roomHeightMax = 8;
    public GameObject enemyPrefab;
    public int enemyCount = 3;

    private List<Rect> rooms;

    void Start()
    {
        rooms = new List<Rect>();
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < roomCount; i++)
        {
            int roomWidth = Random.Range(roomWidthMin, roomWidthMax);
            int roomHeight = Random.Range(roomHeightMin, roomHeightMax);
            int roomX = Random.Range(0, dungeonWidth - roomWidth);
            int roomY = Random.Range(0, dungeonHeight - roomHeight);

            Rect room = new Rect(roomX, roomY, roomWidth, roomHeight);

            bool overlap = false;
            foreach (Rect otherRoom in rooms)
            {
                if (room.Overlaps(otherRoom))
                {
                    overlap = true;
                    break;
                }
            }

            if (!overlap)
            {
                rooms.Add(room);
                CreateRoom(room);
            }
        }

        for (int i = 1; i < rooms.Count; i++)
        {
            CreateCorridor(rooms[i].center, rooms[i - 1].center);
        }

        for (int i = 0; i < enemyCount; i++)
        {
            int roomIndex = Random.Range(0, rooms.Count);
            Rect room = rooms[roomIndex];
            Vector3 position = new Vector3(Random.Range(room.xMin, room.xMax), Random.Range(room.yMin, room.yMax), 0);
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }
    }

    void CreateRoom(Rect room)
    {
        for (int x = (int)room.xMin; x < (int)room.xMax; x++)
        {
            for (int y = (int)room.yMin; y < (int)room.yMax; y++)
            {
                Instantiate(floorPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    void CreateCorridor(Vector2 start, Vector2 end)
    {
        Vector2 current = start;
        while (current != end)
        {
            if ((int)current.x != (int)end.x)
                current.x += (end.x > current.x) ? 1 : -1;
            else if ((int)current.y != (int)end.y)
                current.y += (end.y > current.y) ? 1 : -1;

            Instantiate(floorPrefab, new Vector3(current.x, current.y, 0), Quaternion.identity);
        }
    }
}

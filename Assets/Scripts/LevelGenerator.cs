using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public int distanceToEnd;
    public Color startColor;
    public Color endColor;

    public Transform generatorPoint;
    public float xOffset = 18;
    public float yOffset = 10;
    public LayerMask whatIsRoom;

    public RoomPrefabs rooms;

    public RoomCenter startCenter;
    public RoomCenter endCenter;
    public RoomCenter[] roomCenters;

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
    private Direction nextDirection;
    private GameObject endRoom;
    private List<GameObject> layoutRoomObjects = new List<GameObject>();
    private List<GameObject> generatedOutlines = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;

        for (int i = 0; i < distanceToEnd; ++i)
        {
            nextDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();
            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            if (i == distanceToEnd - 1)
            {
                endRoom = newRoom;
            }
            else
            {
                layoutRoomObjects.Add(newRoom);
            }
        }
        endRoom.GetComponent<SpriteRenderer>().color = endColor;

        CreateRoomOutline(Vector3.zero);
        foreach(GameObject room in layoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);

        foreach(GameObject room in generatedOutlines)
        {
            RoomCenter newCenter;
            if (room.transform.position == Vector3.zero)
            {
                newCenter = Instantiate(startCenter, room.transform.position, transform.rotation);
            }
            else if (room.transform.position == endRoom.transform.position)
            {
                newCenter = Instantiate(endCenter, room.transform.position, transform.rotation);
            }
            else
            {
                int centerNum = Random.Range(0, roomCenters.Length);
                newCenter = Instantiate(roomCenters[centerNum], room.transform.position, transform.rotation);
            }
            newCenter.myRoom = room.GetComponent<Room>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveGenerationPoint()
    {
        switch(nextDirection)
        {
            case Direction.Up:
                generatorPoint.position += new Vector3(0, yOffset, 0);
                break;
            case Direction.Down:
                generatorPoint.position -= new Vector3(0, yOffset, 0);
                break;
            case Direction.Right:
                generatorPoint.position += new Vector3(xOffset, 0, 0);
                break;
            case Direction.Left:
                generatorPoint.position -= new Vector3(xOffset, 0, 0);
                break;
        }
    }

    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), .2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition - new Vector3(0, yOffset, 0), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition - new Vector3(xOffset, 0, 0), .2f, whatIsRoom);

        int neighborCount = (roomAbove ? 1 : 0) + (roomBelow ? 1 : 0) + (roomRight ? 1 : 0) + (roomLeft ? 1 : 0);
        switch(neighborCount)
        {
            case 0:
                Debug.LogError("No neighbors when generating rooms");
                break;
            case 1:
                if(roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                } else if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                }
                else if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                }
                else if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                }
                break;
            case 2:
                if(roomLeft && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                } else if (roomAbove && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                } else if (roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                } else if (roomBelow && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
                } else if (roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftDown, roomPosition, transform.rotation));
                } else if (roomAbove && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
                }
                break;
            case 3:
                if (!roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, transform.rotation));
                } else if (!roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpLeftDown, roomPosition, transform.rotation));
                } else if (!roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftDownRight, roomPosition, transform.rotation));
                } else if (!roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                }
                break;
            case 4:
                generatedOutlines.Add(Instantiate(rooms.quad, roomPosition, transform.rotation));
                break;
        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleLeft;
    public GameObject singleRight;
    public GameObject singleUp;
    public GameObject singleDown;
    public GameObject doubleLeftRight;
    public GameObject doubleUpDown;
    public GameObject doubleUpRight;
    public GameObject doubleRightDown;
    public GameObject doubleLeftDown;
    public GameObject doubleLeftUp;
    public GameObject tripleLeftUpRight;
    public GameObject tripleUpRightDown;
    public GameObject tripleLeftDownRight;
    public GameObject tripleUpLeftDown;
    public GameObject quad;
}

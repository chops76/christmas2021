using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public bool openWhenEnemiesCleared;
    public Room myRoom;

    // Start is called before the first frame update
    void Start()
    {
        if(openWhenEnemiesCleared)
        {
            myRoom.closeOnEnter = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myRoom.roomActive && enemies.Count > 0 && openWhenEnemiesCleared)
        {
            enemies = enemies.Where(x => x != null).ToList();
        }

        if (enemies.Count == 0)
        {
            myRoom.OpenDoors();
        }
    }
}

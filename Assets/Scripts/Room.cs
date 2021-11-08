using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeOnEnter;
    public bool openWhenEnemiesCleared;
    public GameObject[] doors;
    public List<GameObject> enemies = new List<GameObject>();

    private bool roomActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(roomActive && enemies.Count > 0 && openWhenEnemiesCleared)
        {
            enemies = enemies.Where(x => x != null).ToList();
        }

        if(enemies.Count == 0)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CameraController.instance.SetTarget(transform);
            if (closeOnEnter && enemies.Count > 0)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }

            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive = false;
        }
    }
}

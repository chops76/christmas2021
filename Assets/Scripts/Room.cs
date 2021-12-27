using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeOnEnter;
    public GameObject[] doors;
    public GameObject hider;

    [HideInInspector]
    public bool roomActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }

    public void RoomEntered()
    {
        hider.SetActive(false);
        CameraController.instance.SetTarget(transform);
        if (closeOnEnter)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }
        }

        roomActive = true;
    }

    public void RoomExited()
    {
        roomActive = false;
    }
}

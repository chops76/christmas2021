using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    private Room parentRoom;

    // Start is called before the first frame update
    void Start()
    {
        parentRoom = transform.parent.gameObject.GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered on");
        if (other.tag == "Player")
        {
            parentRoom.RoomEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Triggered off");
        if (other.tag == "Player")
        {
            parentRoom.RoomExited();
        }
    }
}

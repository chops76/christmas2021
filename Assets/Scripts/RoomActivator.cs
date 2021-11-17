using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    private Room parentRoom;

    private void Awake()
    {
        parentRoom = transform.parent.gameObject.GetComponent<Room>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
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

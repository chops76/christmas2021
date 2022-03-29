using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{
    public GunPickup[] potentialPickups;

    public SpriteRenderer theSR;
    public Sprite openImage;
    public GameObject notification;
    public Transform spawnPoint;
    public float scaleSpeed = 2f;

    private bool inArea;
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        inArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!opened && inArea && Input.GetKeyDown(KeyCode.E))
        {
            int gunIndex = Random.Range(0, potentialPickups.Length);
            Instantiate(potentialPickups[gunIndex], spawnPoint.position, spawnPoint.rotation);
            theSR.sprite = openImage;
            notification.SetActive(false);
            opened = true;
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        if (opened)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!opened && other.tag == "Player")
        {
            notification.SetActive(true);
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(false);
            inArea = false;
        }
    }
}

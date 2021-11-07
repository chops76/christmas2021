using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    public bool shouldDrop;
    public GameObject[] dropItems;
    public int itemDropPercent;

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
        if((other.tag == "Player" && PlayerController.instance.isDashing()) || other.tag == "PlayerBullet") 
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.BoxBreaking);
            Destroy(gameObject);

            int piecesToDrop = Random.Range(1, maxPieces + 1);
            for (int i = 0; i < piecesToDrop; i++)
            {
                Instantiate(brokenPieces[Random.Range(0, brokenPieces.Length)],
                    transform.position, transform.rotation);
            }

            if (shouldDrop && Random.Range(0, 100) < itemDropPercent)
            {
                int randomItem = Random.Range(0, dropItems.Length);
                Instantiate(dropItems[randomItem], transform.position, transform.rotation);
            }
        }
    }
}

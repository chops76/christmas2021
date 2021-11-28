using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public float collectableDelay = .5f;

    private float delayCount;

    private void Awake()
    {
        delayCount = collectableDelay;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delayCount > 0)
        {
            delayCount -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && delayCount <= 0)
        {
            LevelManager.instance.PickupCoins(value);
            AudioManager.instance.PlaySFX(AudioManager.SFX.PickupCoin);
            Destroy(gameObject);
        }
    }
}

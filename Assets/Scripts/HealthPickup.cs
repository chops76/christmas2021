using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
    public float collectableDelay = .5f;

    private float delayCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        delayCount = collectableDelay;
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
            AudioManager.instance.PlaySFX(AudioManager.SFX.PickupHealth);
            PlayerHealthController.instance.healPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}

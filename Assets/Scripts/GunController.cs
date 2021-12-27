using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots;

    public string weaponName;
    public Sprite gunUI;
    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.isPaused || !PlayerController.instance.canMove)
        {
            return;
        }

        if (shotCounter >= 0)
        {
            shotCounter -= Time.deltaTime;
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.Shoot1);
            shotCounter = timeBetweenShots;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}

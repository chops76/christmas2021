using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D myRB;
    public Animator anim;
    public float moveSpeed;
    public int health = 150;
    public GameObject[] deathSplatters;
    public GameObject damageEffect;
    public bool canShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    public SpriteRenderer body;

    public float rangeToChasePlayer;
    public float fireRange;
    private Vector3 moveDirection;
    private float fireCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                moveDirection.Normalize();
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            myRB.velocity = moveDirection * moveSpeed;

            if (canShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < fireRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    AudioManager.instance.PlaySFX(AudioManager.SFX.Shoot2);
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        } else {
            myRB.velocity = Vector2.zero;
        }
        anim.SetBool("isMoving", moveDirection != Vector3.zero);
    }

    public void DamageEnemy(int damage_amount)
    {
        Instantiate(damageEffect, transform.position, transform.rotation);
        health -= damage_amount;
        if (health <= 0)
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.EnemyDeath);
            Object.Destroy(gameObject);
            Quaternion rotation = Quaternion.Euler(0, 0, 90 * Random.Range(0, 4));
            Instantiate(deathSplatters[Random.Range(0,deathSplatters.Length)], transform.position, rotation);
        } else
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.EnemyHurt);
        }
    }
}

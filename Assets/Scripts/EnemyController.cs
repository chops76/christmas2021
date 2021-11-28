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

    [Header("Chase Player")]
    public bool shouldChase;
    public float rangeToChasePlayer;
    [Header("Run Away")]
    public bool shouldRunAway;
    public float rangeToRunAway;
    [Header("Wander")]
    public bool shouldWander;
    public float wanderLength;
    public float pauseLength;
    [Header("Patrol")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    [Header("Droppables")]
    public bool shouldDrop;
    public GameObject[] dropItems;
    public int itemDropPercent;

    private float wanderCounter;
    private float pauseCounter;
    private Vector3 wanderDirection;

    private int currentPatrolPoint;

    public float fireRange;
    private Vector3 moveDirection;
    private float fireCounter;

    // Start is called before the first frame update
    void Start()
    {
        if(shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChase)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                moveDirection.Normalize();
            }
            else if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToRunAway && shouldRunAway)
            {
                moveDirection = (PlayerController.instance.transform.position - transform.position) * -1;
                moveDirection.Normalize();
            }
            else if (shouldWander)
            {
                if (wanderCounter > 0)
                {
                    wanderCounter -= Time.deltaTime;
                    moveDirection = wanderDirection;
                    if (wanderCounter <= 0)
                    {
                        pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                    }
                } else if (pauseCounter > 0)
                {
                    pauseCounter -= Time.deltaTime;
                    if(pauseCounter <= 0)
                    {
                        wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);
                        wanderDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
                        wanderDirection.Normalize();
                    }
                }
            }
            else if (shouldPatrol)
            {
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                moveDirection.Normalize();

                if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f)
                {
                    currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
                }
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
            if (shouldDrop && Random.Range(0, 100) < itemDropPercent)
            {
                int randomItem = Random.Range(0, dropItems.Length);
                Instantiate(dropItems[randomItem], transform.position, transform.rotation);
            }
        } else
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.EnemyHurt);
        }
    }
}

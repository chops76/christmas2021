using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float dashSpeed = 8;
    public float dashLength = .5f;
    public float dashCoolDown = 1f;
    public float dashInvinc = .5f;
    private Vector2 moveInput;
    private Camera mainCamera;
    private float shotCounter;
    private float activeMoveSpeed;
    private float dashCounter;
    private float dashCoolCounter;

    public Rigidbody2D myRB;
    public Transform gunArm;
    public Animator anim;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots;

    [HideInInspector]
    public bool canMove = true;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        activeMoveSpeed = moveSpeed;
        dashCoolCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.instance.isPaused)
        {
            return;
        }
        if(!canMove)
        {
            myRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        myRB.velocity = moveInput * activeMoveSpeed;
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        if ( offset.x < 0 )
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), 
                transform.localScale.y, transform.localScale.z);
            gunArm.transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x),
                -1 * Mathf.Abs(transform.localScale.y), transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 
                transform.localScale.y, transform.localScale.z);
            gunArm.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        if(Input.GetButtonDown("Fire1"))
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.Shoot1);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetButton("Fire1"))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.Shoot1);
                shotCounter = timeBetweenShots;
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0 && dashCounter <= 0)
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.PlayerDash);
            anim.SetTrigger("dash");
            activeMoveSpeed = dashSpeed;
            dashCounter = dashLength;
            PlayerHealthController.instance.SetInvincible(dashLength);
        }

        if ( dashCounter > 0 )
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0 )
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCoolDown;
            }
        }

        if ( dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        anim.SetBool("isMoving", moveInput != Vector2.zero);
    }

    public bool isDashing()
    {
        return dashCounter > 0;
    }

    public void setCharacterAlpha(float alpha)
    {
        SpriteRenderer[] children = PlayerController.instance.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer child in children)
        {
            Color newColor = child.color;
            newColor.a = alpha;
            child.color = newColor;
        }
    }
}

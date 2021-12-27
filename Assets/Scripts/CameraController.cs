using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float moveSpeed;

    public Camera mainCamera;
    public Camera bigMapCamera;

    public bool bigMapActive;

    private Transform target;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            } else
            {
                DeactivateBigMap();
            }
        }
    }

    public void SetTarget(Transform new_target)
    {
        target = new_target;
    }

    public void ActivateBigMap()
    {
        if(LevelManager.instance.isPaused)
        {
            return;
        }
        bigMapActive = true;
        bigMapCamera.enabled = true;
        mainCamera.enabled = false;
        UIController.instance.mapDisplay.SetActive(false);
        UIController.instance.bigMapText.SetActive(true);

        PlayerController.instance.canMove = false;
        Time.timeScale = 0f;
    }

    public void DeactivateBigMap()
    {
        if(LevelManager.instance.isPaused)
        {
            return;
        }
        bigMapActive = false;
        bigMapCamera.enabled = false;
        mainCamera.enabled = true;
        UIController.instance.mapDisplay.SetActive(true);
        UIController.instance.bigMapText.SetActive(false);

        PlayerController.instance.canMove = true;
        Time.timeScale = 1f;
    }
}

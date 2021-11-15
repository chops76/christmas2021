using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoadNext = 4.0f;
    public string nextLevel;

    [HideInInspector]
    public bool isPaused = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }

    public IEnumerator LevelEnd()
    {
        PlayerController.instance.canMove = false;
        UIController.instance.StartFadeToBlack();
        AudioManager.instance.PlayLevelWin();
        yield return new WaitForSeconds(waitToLoadNext);
        SceneManager.LoadScene(nextLevel);
    }

    public void PauseToggle()
    {
        isPaused = !isPaused;
        UIController.instance.pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}

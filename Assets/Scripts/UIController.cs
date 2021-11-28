using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Slider healthSider;
    public Text healthText;
    public Text coinText;
    public GameObject deathScreen;
    public Image fadeScreen;
    public float fadeSpeed;
    public string newGameScene;
    public string mainMenuScene;
    public GameObject pauseMenu;

    private bool fadeToBlack;
    private bool fadeOutBlack;

    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeToBlack = false;
        fadeOutBlack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
            Color newColor = fadeScreen.color;
            newColor.a = Mathf.MoveTowards(newColor.a, 0f, fadeSpeed * Time.deltaTime);
            fadeScreen.color = newColor;
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }
        if (fadeToBlack)
        {
            Color newColor = fadeScreen.color;
            newColor.a = Mathf.MoveTowards(newColor.a, 1f, fadeSpeed * Time.deltaTime);
            fadeScreen.color = newColor;
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Resume()
    {
        LevelManager.instance.PauseToggle();
    }
}

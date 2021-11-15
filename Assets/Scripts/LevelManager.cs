using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoadNext = 4.0f;
    public string nextLevel;

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
        
    }

    public IEnumerator LevelEnd()
    {
        PlayerController.instance.canMove = false;
        UIController.instance.StartFadeToBlack();
        AudioManager.instance.PlayLevelWin();
        yield return new WaitForSeconds(waitToLoadNext);
        SceneManager.LoadScene(nextLevel);
    }
}

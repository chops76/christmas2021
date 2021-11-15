using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public float waitForAnyKey = 2.0f;
    public GameObject anyKeyText;
    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waitForAnyKey > 0f)
        {
            waitForAnyKey -= Time.deltaTime;
            if(waitForAnyKey <= 0f)
            {
                Debug.Log("Turning on text");
                anyKeyText.SetActive(true);
            }
        } else
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }


}

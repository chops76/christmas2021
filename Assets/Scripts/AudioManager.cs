using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource levelMusic;
    public AudioSource gameOverMusic;
    public AudioSource winMusic;

    public AudioSource[] sfx;

    public enum SFX
    {
        BoxBreaking,
        EnemyDeath,
        EnemyHurt,
        Explosion,
        Impact,
        PickupCoin,
        PickupGun,
        PickupHealth,
        PlayerDash,
        PlayerDeath,
        PlayerDie,
        PlayerHurt,
        Shoot1,
        Shoot2,
        Shoot3,
        Shoot4,
        Shoot5,
        Shoot6,
        ShopBuy,
        ShopNotEnough,
        WarpOut
    }

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

    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlayLevelWin()
    {
        levelMusic.Stop();
        winMusic.Play();
    }

    public void PlaySFX(SFX effect)
    {
        sfx[(int)effect].Stop();
        sfx[(int)effect].Play();
    }
}

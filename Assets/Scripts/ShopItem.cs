using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    public enum UpgradeType
    {
        HealthRestore,
        HealthUpgrade,
        Weapon
    }

    public GameObject buyMessage;
    public UpgradeType upgradeType;
    public int upgradeAmount;
    public int itemCost;

    private bool inBuyZone;

    // Start is called before the first frame update
    void Start()
    {
        inBuyZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (LevelManager.instance.numCoins >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);
                    if (upgradeType == UpgradeType.HealthRestore)
                    {
                        PlayerHealthController.instance.healPlayer(
                            PlayerHealthController.instance.maxHealth);
                    }
                    if (upgradeType == UpgradeType.HealthUpgrade)
                    {
                        PlayerHealthController.instance.increaseMaxHealth(upgradeAmount);
                    }
                    gameObject.SetActive(false);
                    inBuyZone = false;
                    AudioManager.instance.PlaySFX(AudioManager.SFX.ShopBuy);
                } else
                {
                    AudioManager.instance.PlaySFX(AudioManager.SFX.ShopNotEnough);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}

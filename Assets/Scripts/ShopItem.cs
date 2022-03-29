using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GunController[] potentialGuns;
    private GunController theGun;
    public SpriteRenderer gunSprite;
    public Text gunPurchaseText;

    private bool inBuyZone;

    // Start is called before the first frame update
    void Start()
    {
        inBuyZone = false;
        if (upgradeType == UpgradeType.Weapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];
            gunSprite.sprite = theGun.shopSprite;
            itemCost = theGun.cost;
            gunPurchaseText.text = theGun.weaponName + "\n - " + itemCost + " Gold -";
        }
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
                    if (upgradeType == UpgradeType.Weapon)
                    {
                        PlayerController.instance.pickupGun(theGun);
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

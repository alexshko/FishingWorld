using alexshko.fishingworld.Store;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemMessageText : MonoBehaviour
{
    public Transform ItemNameRef;
    public Transform ItemDescriptionRef;
    public Transform ItemCoinsPriceRef;
    public Transform ItemEmeraldsPriceRef;
    public Transform ItemImageRef;
    public Transform BtnBuyRef;
    public Transform BtnEquipRef;

    private string itemName;
    private string itemDescr;
    private int coinsPrice;
    private int emeraldsPrice;
    private GameObject imageLink;

    private IStoreItem itemToBuy;

    private string ItemName
    {
        get => itemName;
        set
        {
            itemName = value;
            ItemNameRef.GetComponent<Text>().text = value;
        }
    }
    private string ItemDescr
    {
        get => itemDescr;
        set
        {
            itemDescr = value;
            ItemDescriptionRef.GetComponent<Text>().text = value;
        }
    }
    private int CoinsPrice
    {
        get => coinsPrice;
        set
        {
            coinsPrice = value;
            ItemCoinsPriceRef.GetComponent<Text>().text = value.ToString();
        }
    }
    private int EmeraldsPrice
    {
        get => emeraldsPrice;
        set
        {
            emeraldsPrice = value;
            ItemEmeraldsPriceRef.GetComponent<Text>().text = value.ToString();
        }
    }

    private GameObject ImageLink
    {
        get => imageLink;
        set
        {
            imageLink = value;

            //if there is currently a ui pic then destroy it.
            foreach (var curImage in ItemImageRef.GetComponentsInChildren<Image>())
            {
                if (curImage != null && curImage.tag!="UiCircleSprite")
                {
                    Destroy(curImage.gameObject);
                }

            } 

            GameObject newImage = Instantiate(ImageLink, ItemImageRef);
            newImage.SetActive(true);
        }
    }

    public IStoreItem ItemToBuy
    {
        get => itemToBuy;
        set
        {
            itemToBuy = value;
            ItemName = value.ItemName;
            ItemDescr = value.ItemDesc;
            CoinsPrice = value.CoinsPrice;
            EmeraldsPrice = value.EmeraldPrice;
            ImageLink = value.ImageLink;

            UpdateButtons();
        }
    }

    private void UpdateButtons()
    {
        //choose the correct button, if can equip or has to buy first.
        BtnBuyRef.gameObject.SetActive(ItemToBuy.isCapableBuying);
        BtnEquipRef.gameObject.SetActive(ItemToBuy.isAlreadyBaught);
    }

    public void BuyItem()
    {
        Debug.Log("Bought item: " + ItemToBuy.ItemName);
        ItemToBuy.Buy();

        UpdateButtons();
        ////close the window of the message:
        //gameObject.SetActive(false);
    }

    public void EquipItem()
    {
        Debug.Log("Equiped item: " + ItemToBuy.ItemName);
        ItemToBuy.Equip();

        //close the window of the message:
        gameObject.SetActive(false);
    }
}
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
    public Image ItemImageRef;

    private string itemName;
    private string itemDescr;
    private int coinsPrice;
    private int emeraldsPrice;
    private string imageLink;

    private IStoreItem itemToBuy;

    private Coroutine ImageLoadRoutine = null;

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

    private string ImageLink
    {
        get => imageLink;
        set
        {
            imageLink = value;

            //Load the Image with coroutine:
            if (ImageLoadRoutine != null)
            {
                StopCoroutine(ImageLoadRoutine);
            }
            ImageLoadRoutine = StartCoroutine(LoadImage());
        }
    }

    private IEnumerator LoadImage()
    {
        ResourceRequest re = Resources.LoadAsync<Sprite>(ImageLink);
        while (!re.isDone)
        {
            yield return null;
        }
        ItemImageRef.sprite = re.asset as Sprite;
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
        }
    }

    public void BuyItem()
    {
        Debug.Log("Bought item: " + ItemToBuy.ItemName);
    }
}
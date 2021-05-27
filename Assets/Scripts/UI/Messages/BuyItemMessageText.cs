using System.Collections;
using System.Collections.Generic;
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

    public string ItemName
    {
        get => itemName;
        set
        {
            itemName = value;
            ItemNameRef.GetComponent<Text>().text = value;
        }
    }
    public string ItemDescr
    {
        get => itemDescr;
        set
        {
            itemDescr = value;
            ItemDescriptionRef.GetComponent<Text>().text = value;
        }
    }
}

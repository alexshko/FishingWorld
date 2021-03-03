using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserStats : MonoBehaviour
{
    public Transform CoinsRef;
    public Transform EmeraldsRef;

    private int coins;
    public int Coins { 
        get { 
            return coins; 
        } 
        set { 
            coins = value;
            CoinsRef.GetComponent<TMP_Text>().text = value.ToString("F0");
        } 
    }
    private int emeralds;
    public int Emeralds
    {
        get
        {
            return emeralds;
        }
        set
        {
            emeralds = value;
            EmeraldsRef.GetComponent<TMP_Text>().text = value.ToString("F0");
        }
    }
    public int RoyalStarts { get; set; }

    public int FishCaught { get; set; }

    public static UserStats instance;

    private void Awake()
    {
        instance = this;
    }

}

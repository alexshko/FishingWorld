using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStats : MonoBehaviour
{
    public int Coins { get; set; }
    public int Emeralds { get; set; }
    public int RoyalStarts { get; set; }

    public int FishCaught { get; set; }

    public static UserStats instance;

    private void Awake()
    {
        instance = this;
    }

}

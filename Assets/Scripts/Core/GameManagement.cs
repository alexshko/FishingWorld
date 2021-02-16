using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance;

    public Transform FisherGuy;

    [Tooltip("the place to cast the rod")]
    public Transform FishingSpot;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void CastRod()
    {
        Debug.LogFormat("cast a rod");
        if (FishingSpot)
        {
            FisherGuy.GetComponent<FisherGuyController>().CastRod(FishingSpot.position);
        }
    }
}

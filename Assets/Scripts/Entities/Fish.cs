using UnityEngine;

public class Fish : MonoBehaviour
{
    [Tooltip("the name of the fish")]
    public string Name;
    [Tooltip("How dificult to catch the fish. the higher the value the more it will fight before pulled out.")]
    [Range(0,10)]
    public float DiffucltyToCatch;
    [Tooltip("the consentration of the fish in the lake")]
    [Range(0,100)]
    public float popularity;
    [Tooltip("how much Coins one can earn from it")]
    public int CoinsWorth;
    [Tooltip("how much Royal Starts one can earn from it. Only the first time the fish is caught.")]
    public int RoyalStarsWorth;

}

using UnityEngine;
namespace alexshko.fishingworld.Enteties.Fish {
    public class Fish : MonoBehaviour
    {
        //public int id;
        //[Tooltip("the name of the fish")]
        //public string Name;
        //[Tooltip("How dificult to catch the fish. the higher the value the more it will fight before pulled out.")]
        //[Range(0, 10)]
        //public float DiffucltyToCatch;
        ////[Tooltip("the consentration of the fish in the lake")]
        ////[Range(0,100)]
        ////public float popularity;
        //[Tooltip("the weight of the fish")]
        //public float weight;
        //[Tooltip("how much Coins one can earn from it per pound")]
        //public int CoinsWorth;
        //[Tooltip("how much Royal Starts one can earn from it. Only the first time the fish is caught.")]
        //public int RoyalStarsWorth;

        public ScriptableObjectFish FishData;

        [SerializeField]
        private float weight;

        private void Awake()
        {
            float max = FishData.MaxWeight;
            float min = 0.5f;
            weight = Random.Range(min, max);
        }
    }
}

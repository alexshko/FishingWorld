using alexshko.fishingworld.Core;
using UnityEngine;
namespace alexshko.fishingworld.Enteties.Fishes {
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
        public float weight;

        private bool tookBait;
        public bool TookBait { get { return tookBait; } }

        //the current resstance the fish apllies. will be used in the machnism.
        public float CurrentResist{ get; set; }

        private void Awake()
        {
            float maxWeight = FishData.MaxWeight;
            float meanWeight = FishData.MeanWeight;
            float minWeight = 0.5f;
            //weight = Random.Range(min, max);

            //randomize with normal distribution of (meanWeight, 10f) and make sure its not more than maxWeight and not less than 0.5 kilos.
            weight = Mathf.Clamp((float)RandomizeWeight(meanWeight, 10f), minWeight, maxWeight);

            tookBait = false;
            CurrentResist = 0;
        }

        private void Update()
        {
            if (!tookBait)
            {
                RaycastHit[] hits;
                hits = Physics.SphereCastAll(transform.parent.position, 0.2f, transform.up, 0.2f);

                if ((hits != null) && (hits.Length > 0))
                {
                    foreach (var hit in hits)
                    {
                        if (hit.collider.tag == "LineEnd")
                        {
                            AttachToLineEnd(hit.collider.transform);
                            GameManagement.Instance.HandleFishTookBait(transform);
                        }
                    }
                }
            }
        }

        private void AttachToLineEnd(Transform lineEnd)
        {
            transform.parent = lineEnd;
            transform.localPosition = Vector3.zero;
            tookBait = true;
        }

        private double RandomizeWeight(float mean=10, float stdDev=10)
        {
            System.Random rand = new System.Random(); //reuse this if you are generating many
            float u1 = 1.0f - (float)(rand.NextDouble()); //uniform(0,1] random doubles
            float u2 = 1.0f - (float)(rand.NextDouble()); //uniform(0,1] random doubles
            double randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                         Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
    }
}

using UnityEngine;
namespace alexshko.fishingworld.Enteties.FishingRod
{
    public class FishingLineHinge : MonoBehaviour
    {
        public Transform FishPoint;

        public Transform RodFishingLine;

        [Tooltip("the top of the fishing rod so it will always update its place there")]
        public Transform FishingRodTop;

        private float initFishDistance = 2.5f;

        private void Awake()
        {
            //makes the Hinge invisable.
            GetComponent<MeshRenderer>().enabled = false;

            float initDist = initFishDistance;
            //put the Fish right in front of the Hinge:
            //FishPoint.position = transform.position + transform.forward * initDist;

            //put the fish in intial place.
            FishPoint.position = transform.position - transform.right * 0.2f - transform.up* (initDist / Mathf.Sqrt(2));
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = FishingRodTop.position;

            float dist = Vector3.Distance(transform.position, FishPoint.position);
            Debug.Log("the distance between the rod and the Fish: " + dist);
            transform.LookAt(FishPoint);
            RodFishingLine.localScale = new Vector3(RodFishingLine.localScale.x, dist/2, RodFishingLine.localScale.z);
            RodFishingLine.localPosition = new Vector3(RodFishingLine.localPosition.x, dist / 2, RodFishingLine.localPosition.z);
        }
    }
}

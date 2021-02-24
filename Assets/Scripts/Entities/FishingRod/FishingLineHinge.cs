using UnityEngine;
namespace alexshko.fishingworld.Enteties.FishingRod
{
    public class FishingLineHinge : MonoBehaviour
    {
        [Tooltip("the transform that repesents the end tip of the fishing line.")]
        public Transform EndOfLine;

        [Tooltip("the rod fishing line")]
        public Transform RodFishingLine;
        [Tooltip("The position to put the end of the line so it will be in loose stance")]
        public Transform EndOfLineLooseStancePosition;

        [Tooltip("the top of the fishing rod so it will always update its place there")]
        public Transform FishingRodTop;

        private bool isInLooseState;


        //private float initFishDistance = 2.5f;

        //public Vector3 NormalStanceToCast { get
        //    {
        //        return (transform.position - transform.right * 0.2f - transform.up * (initFishDistance / Mathf.Sqrt(2)));
        //    } }

        private void MoveLineToLooseStance()
        {
            EndOfLine.position = EndOfLineLooseStancePosition.position;
            isInLooseState = true;
        }

        private void MoveLineToFollowFish()
        {
            isInLooseState = false;
        }

        private void Awake()
        {
            //makes the Hinge invisable.
            GetComponent<MeshRenderer>().enabled = false;

            //put the fish in intial place.
            MoveLineToLooseStance();
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = FishingRodTop.position;

            if (isInLooseState)
            {
                EndOfLine.position = EndOfLineLooseStancePosition.position;
            }

            float dist = Vector3.Distance(transform.position, EndOfLine.position);
            Debug.Log("the distance between the rod and the Fish: " + dist);
            transform.LookAt(EndOfLine);
            RodFishingLine.localScale = new Vector3(RodFishingLine.localScale.x, dist / 2, RodFishingLine.localScale.z);
            RodFishingLine.localPosition = new Vector3(RodFishingLine.localPosition.x, dist / 2, RodFishingLine.localPosition.z);
        }
    }
}

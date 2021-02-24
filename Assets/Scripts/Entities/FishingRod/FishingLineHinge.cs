using System.Collections;
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
        private Coroutine MovingAnim;


        //private float initFishDistance = 2.5f;

        //public Vector3 NormalStanceToCast { get
        //    {
        //        return (transform.position - transform.right * 0.2f - transform.up * (initFishDistance / Mathf.Sqrt(2)));
        //    } }

        private void Awake()
        {
            //makes the Hinge invisable.
            GetComponent<MeshRenderer>().enabled = false;

            //put the fish in intial place.
            //PutEndOfLineInNewPosition(EndOfLineLooseStancePosition.position);
            EndOfLine.position = EndOfLineLooseStancePosition.position;
            isInLooseState = true;
        }

        //Update is called once per frame
        void LateUpdate()
        {
            transform.position = FishingRodTop.position;

            //if (isInLooseState)
            //{
            //    PutEndOfLineInNewPosition(EndOfLineLooseStancePosition.position);
            //}
            //else
            //{
            //    PutEndOfLineInNewPosition(EndOfLine.position);
            //}
            PutEndOfLineInNewPosition(EndOfLine.position);
        }

        private void PutEndOfLineInNewPosition(Vector3 newPos)
        {
            float dist = Vector3.Distance(transform.position, newPos);
            Debug.Log("the distance between the rod and the Fish: " + dist);
            transform.LookAt(newPos);
            RodFishingLine.localScale = new Vector3(RodFishingLine.localScale.x, dist / 2, RodFishingLine.localScale.z);
            RodFishingLine.localPosition = new Vector3(RodFishingLine.localPosition.x, dist / 2, RodFishingLine.localPosition.z);
        }

        private IEnumerator MoveLineBottomToPosAnim(Vector3 newPos)
        {
            while (Vector3.Distance(EndOfLine.position, newPos) > 1.5f)
            {
                EndOfLine.position = Vector3.Lerp(EndOfLine.position, newPos, Time.deltaTime);
                //PutEndOfLineInNewPosition(EndOfLine.position);
                yield return null;
            }
            EndOfLine.position = newPos;
            yield return null;
        }

        public void CastRod(Vector3 newPos)
        {
            //isInLooseState = false;
            if (MovingAnim!=null)
            {
                StopCoroutine(MovingAnim);
            }
            MovingAnim = StartCoroutine(MoveLineBottomToPosAnim(newPos));
        }

        public void PullRod()
        {
            //isInLooseState = true;
            CastRod(EndOfLineLooseStancePosition.position);
        }
    }
}

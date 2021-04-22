using System.Collections;
using UnityEngine;
using alexshko.fishingworld.Enteties;
using System;
using alexshko.fishingworld.Core;

namespace alexshko.fishingworld.Enteties
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

        [Tooltip("the speed at wich the rod is casted")]
        public float CastSpeed = 1;
        [Tooltip("the speed at wich the rod is pulled")]
        public float PullSpeed = 5;

        private Coroutine MovingAnim;
        private Action OnCoroutineFinished;


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
            UpdatePointOfContactOnLake();
        }

        private void PutEndOfLineInNewPosition(Vector3 newPos)
        {
            float dist = Vector3.Distance(transform.position, newPos);
            Debug.Log("the distance between the rod and the Fish: " + dist);
            transform.LookAt(newPos);
            RodFishingLine.localScale = new Vector3(RodFishingLine.localScale.x, dist / 2, RodFishingLine.localScale.z);
            RodFishingLine.localPosition = new Vector3(RodFishingLine.localPosition.x, dist / 2, RodFishingLine.localPosition.z);
        }

        private void UpdatePointOfContactOnLake()
        {
            float dist = Vector3.Distance(transform.position, EndOfLine.position);
            LayerMask watermask = LayerMask.GetMask("Water");
            Vector3 direction = (EndOfLine.position - transform.position).normalized;
            RaycastHit hit;
            //if found a water, then should make a riplle effect in the contact point
            if (Physics.Raycast(transform.position, direction, out hit,dist, watermask))
            {
                if (hit.collider.GetComponent<Lake>() != null)
                {
                    hit.collider.GetComponent<Lake>().PointOFRippleEffect = hit.point;
                }
            }
            else
            {
                GameManagement.Instance.LevelLake.PointOFRippleEffect = Vector3.zero;
            }
        }

        private IEnumerator MoveLineBottomToPosAnim(Vector3 newPos, float speed = -1)
        {
            if (speed == -1)
            {
                speed = CastSpeed;
            }
            while (Vector3.Distance(EndOfLine.position, newPos) > 0.5f)
            {
                EndOfLine.position = Vector3.Lerp(EndOfLine.position, newPos, speed * Time.deltaTime);
                //PutEndOfLineInNewPosition(EndOfLine.position);
                yield return null;
            }
            EndOfLine.position = newPos;
            yield return null;
            if (OnCoroutineFinished != null)
            {
                OnCoroutineFinished();
            }
        }

        public void CastRod(Vector3 newPos, float speed = -1)
        {
            if (MovingAnim!=null)
            {
                StopCoroutine(MovingAnim);
            }
            MovingAnim = StartCoroutine(MoveLineBottomToPosAnim(newPos, speed));
        }

        public void PullRod()
        {
            //isInLooseState = true;
            CastRod(EndOfLineLooseStancePosition.position, PullSpeed);
        }

        public void AttachFishToEndOfLine(Transform fish)
        {
            fish.parent = EndOfLine;
        }

    }
}

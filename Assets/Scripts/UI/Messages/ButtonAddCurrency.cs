using alexshko.fishingworld.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace alexshko.fishingworld.UI.Messages
{
    public enum Currency { Coins, Emeralds };


    public class ButtonAddCurrency : MonoBehaviour
    {
        [Tooltip("the reference to the effect for when currency is added")]
        public VisualEffect EffectRef;


        [SerializeField]
        private Currency currency;
        [SerializeField]
        private int amount;

        private Vector3 worldResPos;
        public Transform originSphere;

        private void Start()
        {
            if (Currency == Currency.Coins)
            {
                worldResPos = transform.position;
                Vector3 worldDesPos = (UserStats.instance.CoinsRef.parent.position);
                worldDesPos.z -= 1;
                worldResPos.z -= 1;
                EffectRef.SetVector3("EndPosition", worldDesPos);
                EffectRef.transform.position = worldResPos;
                originSphere.transform.position = worldResPos;
            }
            else
            {
                Vector3 worldResPos = FindPositionOfUIElement((transform.position));
                Vector3 worldDesPos = FindPositionOfUIElement((UserStats.instance.EmeraldsRef.position));
                EffectRef.SetVector3("EndPosition", worldDesPos);
                EffectRef.transform.position = worldResPos;
            }
        }

        private void OnDrawGizmos()
        {
            if (Currency == Currency.Coins)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(worldResPos, new Vector3(10, 10, 10));
            }
        }

        private Vector3 FindPositionOfUIElement(Vector3 pos)
        {
            RaycastHit hit;
            Debug.DrawRay(Camera.main.ScreenPointToRay(pos).origin, Camera.main.ScreenPointToRay(pos).direction,Color.blue,5);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit, 1500f))
            {
                return hit.point;
            }
            return Vector3.zero;
        }

        public Currency Currency { 
            get { return currency; } 
        }
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                transform.GetComponentInChildren<Text>().text = value.ToString();
            }
        }

        public void btnAddCurrency()
        {
            if (Currency == Currency.Coins)
            {
                UserStats.instance.Coins += Amount;
            }
            else if (Currency == Currency.Emeralds)
            {
                UserStats.instance.Emeralds += Amount;
            }
            MakeAddCurrencyEffect();


            //hide the message:
            //transform.parent.gameObject.SetActive(false);

            //inform the GameManagement that the fishing cycle is finished:
            //GameManagement.Instance.FinishFishCaughtCycle();
        }

        private void MakeAddCurrencyEffect()
        {
            EffectRef.SendEvent("OnPlay");
            EffectRef.Reinit();
        }
    }
}

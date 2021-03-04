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

        private void Start()
        {
            if (Currency == Currency.Coins)
            {
                Vector3 worldResPos = FindPositionOfUIElement(transform.position);
                Vector3 worldDesPos = FindPositionOfUIElement((UserStats.instance.CoinsRef.position));
                EffectRef.SetVector3("EndPosition", worldDesPos);
                EffectRef.transform.position = worldResPos;
            }
            else
            {
                Vector3 worldResPos = FindPositionOfUIElement((transform.position));
                Vector3 worldDesPos = FindPositionOfUIElement((UserStats.instance.EmeraldsRef.position));
                EffectRef.SetVector3("EndPosition", worldDesPos);
                EffectRef.transform.position = worldResPos;
            }
        }

        private Vector3 FindPositionOfUIElement(Vector3 pos)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit))
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

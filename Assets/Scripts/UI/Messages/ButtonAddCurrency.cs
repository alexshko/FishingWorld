using alexshko.fishingworld.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace alexshko.fishingworld.UI.Messages
{

    public class ButtonAddCurrency : MonoBehaviour
    {
        [Tooltip("the reference to the effect for when currency is added")]
        public VisualEffect EffectRef;

        private Coroutine EffectCoroutine;


        [SerializeField]
        private Currency currency;
        [SerializeField]
        private int amount;

        private void Start()
        {
            if (Currency == Currency.Coins)
            {
                Vector3 worldDesPos = (UserStats.instance.CoinsRef.parent.position);
                EffectRef.SetVector3("EndPosition", worldDesPos);
            }
            else
            {
                Vector3 worldDesPos = (UserStats.instance.EmeraldsRef.parent.position);
                EffectRef.SetVector3("EndPosition", worldDesPos);
            }
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

            //make the visual effect play for few seconds and then close the message window:
            if (EffectCoroutine != null)
            {
                StopCoroutine(EffectCoroutine);
            }
            EffectCoroutine = StartCoroutine(MakeAddCurrencyEffect());
        }

        private IEnumerator MakeAddCurrencyEffect()
        {
            EffectRef.SendEvent("OnPlay");
            Debug.Log("Playen an effect.");
            yield return new WaitForSeconds(3);

            //Update the currency in the UI. after the effect of adding currency defenatly finished.
            ChangeCurrencyInUI();
            yield return null;

            //hide the message:
            transform.parent.parent.gameObject.SetActive(false);

            //inform the GameManagement that the fishing cycle is finished:
            GameManagement.Instance.FinishFishCaughtCycle();

            yield return null;
        }

        private void ChangeCurrencyInUI()
        {
            if (Currency == Currency.Coins)
            {
                UserStats.instance.Coins += Amount;
            }
            else if (Currency == Currency.Emeralds)
            {
                UserStats.instance.Emeralds += Amount;
            }
        }
    }
}

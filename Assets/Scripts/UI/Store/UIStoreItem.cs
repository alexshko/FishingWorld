using alexshko.fishingworld.Store;
using alexshko.fishingworld.UI.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.fishingworld.UI
{
    [RequireComponent(typeof(StoreRodItem))]
    public class UIStoreItem : MonoBehaviour
    {
        public static string txtCoinsPriceName = "txtCoinsPrice";
        public static string txtEmeraldsPriceName = "txtEmeraldsPrice";


        private StoreRodItem StoreItem;
        // Start is called before the first frame update
        void Start()
        {
            InitUIStoreItem();
        }

        private void InitUIStoreItem()
        {
            Text[] TextValues = GetComponentsInChildren<Text>();
            if (TextValues == null || TextValues.Length < 2)
            {
                Debug.LogError("Missing text fields for the cost of item.");
                return;
            }

            StoreItem = GetComponent<StoreRodItem>();
            if (StoreItem == null)
            {
                Debug.LogError("Missing Item info.");
                return;
            }

            foreach (var item in TextValues)
            {
                if (item.name == txtCoinsPriceName)
                {
                    item.text = StoreItem.RodData.CoinsPrice.ToString();
                }
                else if (item.name == txtEmeraldsPriceName)
                {
                    item.text = StoreItem.RodData.EmeraldsPrice.ToString();
                }
            }
        }
        public void BuyRodButton()
        {
            if (StoreItem != null)
            {
                MessageController.instance.ShowMessageBuyStoreItem(StoreItem);

                //close the window of the store:
                GameObject.FindGameObjectWithTag("StorePanel").SetActive(false);
            }
        }
    }
}

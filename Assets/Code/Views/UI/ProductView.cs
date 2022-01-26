using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views.UI
{
    public sealed class ProductView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        
        private string _productID;

        public void Init(UnityAction buyAction, string title, string description, int price, string productID)
        {
            _productID = productID;
            _titleText.text = title;
            _descriptionText.text = description;
            _priceText.text = $"{price} рублей";
            _buyButton.onClick.AddListener(buyAction);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveAllListeners();
        }
    }
}
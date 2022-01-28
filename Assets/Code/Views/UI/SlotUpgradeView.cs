using System;
using Code.Configs.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views.UI
{
    public sealed class SlotUpgradeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _image;
        [SerializeField] private Button _buyButton;
        
        public event Action<SlotUpgradeView, int> OnClick;
        
        public void Init(ItemInfo item)
        {
            _titleText.text = item.Title;
            _image.sprite = item.Icon;
            _buyButton.onClick.AddListener(BuyButton);
        }
        
        public void SetActiveButton(bool active)
        {
            _buyButton.interactable = active;
        }

        private void BuyButton()
        {
            OnClick?.Invoke(this, gameObject.GetInstanceID());
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(BuyButton);
        }
    }
}
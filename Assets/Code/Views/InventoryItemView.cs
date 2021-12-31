using Code.Configs.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views
{
    public sealed class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _image;

        public void Init(ItemInfo item)
        {
            _titleText.text = item.Title;
            _image.sprite = item.Icon;
        }
    }
}
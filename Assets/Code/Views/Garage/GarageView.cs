using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views
{
    public sealed class GarageView : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventory;
        [SerializeField] private Button _exitButton;

        public InventoryView Inventory => _inventory;

        public void Init(UnityAction exitAction)
        {
            _exitButton.onClick.AddListener(exitAction);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}
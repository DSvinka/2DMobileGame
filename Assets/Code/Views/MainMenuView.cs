using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views
{
    public sealed class MainMenuView: MonoBehaviour
    {
        [SerializeField] private ProductsMenuView _productsMenuView;
        
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonDonate;

        public ProductsMenuView ProductsMenuView => _productsMenuView;

        public void Init(UnityAction startGameAction, UnityAction donateAction)
        {
            _buttonStart.onClick.AddListener(startGameAction);
            _buttonDonate.onClick.AddListener(donateAction);
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonDonate.onClick.RemoveAllListeners();
        }
    }
}
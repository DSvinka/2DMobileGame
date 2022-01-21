using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views
{
    public sealed class ProductsMenuView: MonoBehaviour
    {
        [SerializeField] private ProductView[] _productViews;
        [SerializeField] private Button _closeButton;
        
        public ProductView[] ProductViews => _productViews;

        public void Init(UnityAction closeThisAction)
        {
            _closeButton.onClick.AddListener(closeThisAction);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}
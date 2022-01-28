using Code.Views.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views.Garage
{
    public sealed class GarageView : MonoBehaviour
    {
        [SerializeField] private UpgradesView _upgradeMenuView;
        [SerializeField] private Button _exitButton;

        public UpgradesView UpgradeMenuView => _upgradeMenuView;

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
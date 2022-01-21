using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views.UI
{
    public sealed class MainMenuView: MonoBehaviour
    {
        [SerializeField] private ProductsMenuView _productsMenuView;
        [SerializeField] private DailyRewardsView _dailyRewardsView;
        
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonDonate;
        [SerializeField] private Button _buttonDailyRewards;

        public ProductsMenuView ProductsMenuView => _productsMenuView;
        public DailyRewardsView DailyRewardsView => _dailyRewardsView;

        public void Init(UnityAction startGameAction, UnityAction openDonateAction, UnityAction openDailyRewardsAction)
        {
            _buttonStart.onClick.AddListener(startGameAction);
            _buttonDonate.onClick.AddListener(openDonateAction);
            _buttonDailyRewards.onClick.AddListener(openDailyRewardsAction);
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonDonate.onClick.RemoveAllListeners();
            _buttonDailyRewards.onClick.RemoveAllListeners();
        }
    }
}
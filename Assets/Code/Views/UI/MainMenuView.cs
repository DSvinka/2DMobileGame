using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views.UI
{
    public sealed class MainMenuView: MonoBehaviour
    {
        [SerializeField] private DailyRewardsView _dailyRewardsView;
        
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private Button _buttonDailyRewards;
        
        public DailyRewardsView DailyRewardsView => _dailyRewardsView;

        public void Init(UnityAction startGameAction, UnityAction exitGameAction, UnityAction openDailyRewardsAction)
        {
            _buttonStart.onClick.AddListener(startGameAction);
            _buttonExit.onClick.AddListener(exitGameAction);
            _buttonDailyRewards.onClick.AddListener(openDailyRewardsAction);
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
            _buttonDailyRewards.onClick.RemoveAllListeners();
        }
    }
}
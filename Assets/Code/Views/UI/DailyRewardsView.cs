using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views.UI
{
    public sealed class DailyRewardsView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerNewReward;
        [SerializeField] private Slider _timerSlider;
        [SerializeField] private Transform _gridSlotsReward;

        [SerializeField] private SlotRewardView _slotRewardViewPrefab;

        [SerializeField] private Button _getRewardButton;
        [SerializeField] private Button _closeButton;

        public TMP_Text TimerNewReward => _timerNewReward;
        public Slider TimerSlider => _timerSlider;
        public Transform GridSlotsReward => _gridSlotsReward;
        
        public SlotRewardView SlotRewardViewPrefab => _slotRewardViewPrefab;

        public void Init(UnityAction closeThisAction, UnityAction getRewardAction)
        {
            _getRewardButton.onClick.AddListener(getRewardAction);
            _closeButton.onClick.AddListener(closeThisAction);
        }

        private void OnDestroy()
        {
            _getRewardButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        public void SetInteractableButton(bool enable)
        {
            _getRewardButton.interactable = enable;
        }
    }
}
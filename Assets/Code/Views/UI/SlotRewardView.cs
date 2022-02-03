using Code.Configs.Rewards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views.UI
{
    public sealed class SlotRewardView: MonoBehaviour
    {
        [SerializeField] private Image _backgroundSelect;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textDay;
        [SerializeField] private TMP_Text _textRewardCount;

        public void SetData(RewardConfig rewardConfig, string day, bool isSelect)
        {
            _iconCurrency.sprite = rewardConfig.IconCurrency;
            _textDay.text = day;
            _textRewardCount.text = $"{rewardConfig.CountCurrency}";
            
            _backgroundSelect.gameObject.SetActive(isSelect);
        }
    }
}
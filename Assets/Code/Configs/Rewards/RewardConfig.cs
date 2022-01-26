using Code.Enums;
using UnityEngine;

namespace Code.Configs.Rewards
{
    [CreateAssetMenu(fileName = "RewardConfig", menuName = "Configs/Rewards/RewardConfig")]
    public sealed class RewardConfig : ScriptableObject
    {
        public RewardType RewardType;
        public Sprite IconCurrency;
        public int CountCurrency;
    }
}
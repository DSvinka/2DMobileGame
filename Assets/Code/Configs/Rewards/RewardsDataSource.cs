using UnityEngine;

namespace Code.Configs.Rewards
{
    [CreateAssetMenu(fileName = "RewardsDataSource", menuName = "Configs/Rewards/DataSource", order = -1)]
    public sealed class RewardsDataSource: ScriptableObject
    {
        [SerializeField] private RewardConfig[] _rewardConfigs;

        public RewardConfig[] RewardConfigs => _rewardConfigs;
    }
}
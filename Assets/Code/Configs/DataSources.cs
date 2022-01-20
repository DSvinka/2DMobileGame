using Code.Configs.Items;
using Code.Configs.Rewards;
using Code.Configs.Upgrades;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "DataSources", menuName = "Configs/DataSources", order = 5)]
    public sealed class DataSources: ScriptableObject
    {
        [SerializeField] private ItemsDataSource _itemsDataSource;
        [SerializeField] private RewardsDataSource _rewardsDataSource;
        [SerializeField] private SettingsDataSource _settingsDataSource;
        [SerializeField] private UpgradesDataSource _upgradesDataSource;

        public ItemsDataSource ItemsDataSource => _itemsDataSource;
        public RewardsDataSource RewardsDataSource => _rewardsDataSource;
        public SettingsDataSource SettingsDataSource => _settingsDataSource;
        public UpgradesDataSource UpgradesDataSource => _upgradesDataSource;
    }
}
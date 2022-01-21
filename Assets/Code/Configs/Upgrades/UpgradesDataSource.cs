using UnityEngine;

namespace Code.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradesDataSource", menuName = "Configs/Upgrades/DataSource", order = -1)]
    public sealed class UpgradesDataSource: ScriptableObject
    {
        [SerializeField] private UpgradeConfig[] _upgradeConfigs;

        public UpgradeConfig[] UpgradeConfigs => _upgradeConfigs;
    }
}
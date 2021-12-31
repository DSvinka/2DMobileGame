using UnityEngine;

namespace Code.Configs.Items
{
    [CreateAssetMenu(fileName = "UpgradeConfigDataSource", menuName = "Configs/UpgradeConfigDataSource", order = 0)]
    public sealed class UpgradeConfigDataSource: ScriptableObject
    {
        [SerializeField] private UpgradeItemConfig[] _itemConfigs;

        public UpgradeItemConfig[] ItemConfigs => _itemConfigs;
    }
}
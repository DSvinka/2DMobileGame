using Code.Configs.Items;
using Code.Enums;
using UnityEngine;

namespace Code.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeConfig", menuName = "Configs/Upgrades/UpgradeConfig")]
    public sealed class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig _itemConfig;
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private float _valueUpgrade;

        public int ID => _itemConfig.ID;
        public UpgradeType Type => _upgradeType;
        public float ValueUpgrade => _valueUpgrade;
    }
}
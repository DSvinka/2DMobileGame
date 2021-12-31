using UnityEngine;

namespace Code.Configs.Items
{
    [CreateAssetMenu(fileName = "UpgradeItemConfig", menuName = "Configs/UpgradeItemConfig", order = 0)]
    public sealed class UpgradeItemConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig _itemConfig;
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private float _valueUpgrade;

        public int ID => _itemConfig.ID;
        public UpgradeType Type => _upgradeType;
        public float ValueUpgrade => _valueUpgrade;
    }

    public enum UpgradeType
    {
        None = 0,
        Speed = 1,
        Control = 2,
    }
}
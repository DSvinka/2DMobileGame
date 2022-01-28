using Code.Configs.Items;
using Code.Enums;
using UnityEngine;

namespace Code.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeConfig", menuName = "Configs/Upgrades/UpgradeConfig")]
    public sealed class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private ItemConfig _itemConfig;
        [SerializeField] private UpgradeType _upgradeType;
        [SerializeField] private float _valueUpgrade;
        
        [Header("Price")]
        [SerializeField] private CurrencyType _priceCurrency;
        [SerializeField] private int _price;

        public int ID => _id;
        public ItemInfo ItemInfo => _itemConfig.ItemInfo;
        
        public UpgradeType Type => _upgradeType;
        public float ValueUpgrade => _valueUpgrade;

        public CurrencyType PriceCurrency => _priceCurrency;
        public int Price => _price;
    }
}
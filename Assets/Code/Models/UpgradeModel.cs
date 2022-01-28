using Code.Configs.Items;
using Code.Enums;
using Code.Interfaces.Models;

namespace Code.Models
{
    public struct UpgradeModel: IUpgradeModel
    {
        private int _id;
        private ItemInfo _itemInfo;
        
        private UpgradeType _upgradeType;
        private float _valueUpgrade;
        
        private CurrencyType _priceCurrency;
        private int _price;
        

        public int ID => _id;
        public ItemInfo Info => _itemInfo;

        public UpgradeType UpgradeType => _upgradeType;
        public float ValueUpgrade => _valueUpgrade;

        public CurrencyType PriceCurrency => _priceCurrency;
        public int Price => _price;

        public UpgradeModel(int id, ItemInfo itemInfo, UpgradeType upgradeType, float valueUpgrade, CurrencyType priceCurrency, int price)
        {
            _id = id;
            _itemInfo = itemInfo;

            _upgradeType = upgradeType;
            _valueUpgrade = valueUpgrade;

            _priceCurrency = priceCurrency;
            _price = price;
        }
    }
}
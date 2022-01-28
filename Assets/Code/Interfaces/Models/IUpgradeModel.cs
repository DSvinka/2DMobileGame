using Code.Enums;

namespace Code.Interfaces.Models
{
    public interface IUpgradeModel: IItemModel
    {
        public UpgradeType UpgradeType { get; }
        public float ValueUpgrade { get; }
        
        public CurrencyType PriceCurrency { get; }
        public int Price { get; }
    }
}
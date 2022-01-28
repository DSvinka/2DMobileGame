using System.Collections.Generic;
using Code.Configs.Upgrades;
using Code.Interfaces;
using Code.Interfaces.Models;
using Code.Models;

namespace Code.Repositories
{
    public sealed class UpgradesRepository: BaseController, IRepository<int, IUpgradeModel>
    {
        public IReadOnlyDictionary<int, IUpgradeModel> Collection => _upgradesMapByID;

        private Dictionary<int, IUpgradeModel> _upgradesMapByID;

        public UpgradesRepository(UpgradesDataSource upgradesDataSource)
        {
            _upgradesMapByID = new Dictionary<int, IUpgradeModel>();
            PopulateItems(upgradesDataSource);
        }

        private void PopulateItems(UpgradesDataSource upgradesDataSource)
        {
            foreach (var upgradeConfig in upgradesDataSource.UpgradeConfigs)
            {
                if (_upgradesMapByID.ContainsKey(upgradeConfig.ID))
                    continue;
                
                _upgradesMapByID.Add(upgradeConfig.ID, CreateItem(upgradeConfig));
            }
        }

        private IUpgradeModel CreateItem(UpgradeConfig config)
        {
            return new UpgradeModel(config.ID, config.ItemInfo, config.Type, config.ValueUpgrade, config.PriceCurrency, config.Price);
        }

        protected override void OnDispose()
        {
            _upgradesMapByID.Clear();
        }
    }
}
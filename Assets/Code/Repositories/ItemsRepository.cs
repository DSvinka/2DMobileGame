using System.Collections.Generic;
using Code.Configs.Items;
using Code.Interfaces;
using Code.Models;

namespace Code.Repositories
{
    public sealed class ItemsRepository: BaseController, IRepository<int, IItem>
    {
        public IReadOnlyDictionary<int, IItem> Collection => _itemsMapByID;

        private Dictionary<int, IItem> _itemsMapByID;

        public ItemsRepository(ItemsDataSource itemsDataSource)
        {
            _itemsMapByID = new Dictionary<int, IItem>();
            PopulateItems(itemsDataSource);
        }

        private void PopulateItems(ItemsDataSource itemsDataSource)
        {
            foreach (var itemConfig in itemsDataSource.ItemConfigs)
            {
                if (_itemsMapByID.ContainsKey(itemConfig.ID))
                    continue;
                
                _itemsMapByID.Add(itemConfig.ID, CreateItem(itemConfig));
            }
        }

        private IItem CreateItem(ItemConfig itemConfig)
        {
            return new ItemModel(itemConfig.ID, itemConfig.ItemInfo);
        }

        protected override void OnDispose()
        {
            _itemsMapByID.Clear();
        }
    }
}
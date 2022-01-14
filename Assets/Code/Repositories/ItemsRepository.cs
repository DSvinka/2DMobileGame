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

        public ItemsRepository(List<ItemConfig> itemConfigs)
        {
            _itemsMapByID = new Dictionary<int, IItem>();
            PopulateItems(itemConfigs);
        }

        private void PopulateItems(List<ItemConfig> itemConfigs)
        {
            foreach (var itemConfig in itemConfigs)
            {
                if (_itemsMapByID.ContainsKey(itemConfig.ID))
                    continue;
                
                _itemsMapByID.Add(itemConfig.ID, CreateItem(itemConfig));
            }
        }

        private IItem CreateItem(ItemConfig itemConfig)
        {
            var itemInfo = new ItemInfo()
            {
                Title = itemConfig.Title, 
                Icon = itemConfig.Icon,
            };
            
            return new ItemModel(itemConfig.ID, itemInfo);
        }

        protected override void OnDispose()
        {
            _itemsMapByID.Clear();
        }
    }
}
using System;
using System.Collections.Generic;
using Code.Configs.Items;
using Code.Interfaces;
using Code.Interfaces.Repositories;
using Code.Models;
using Code.Repositories;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Garage
{
    public sealed class InventoryController: BaseController
    {
        private readonly ResourcePath _itemViewPath = new ResourcePath() { PathResource = "Prefabs/UI/InventoryItem" };

        private readonly InventoryModel _inventoryModel;
        private readonly InventoryView _inventoryView;
        private readonly IItemsRepository _itemsRepository;

        private readonly Dictionary<int, IItem> _items;

        public InventoryController(List<ItemConfig> itemConfigs, GarageView garageView)
        {
            _inventoryView = garageView.Inventory;
            _inventoryModel = new InventoryModel();
            _itemsRepository = new ItemsRepository(itemConfigs);

            _items = new Dictionary<int, IItem>();
        }

        public void LoadInventory()
        {
            foreach (var item in _itemsRepository.Items.Values)
            {
                _inventoryModel.EquipItem(item);
            }

            var equippedItems = _inventoryModel.GetEquippedItems();
            foreach (var item in equippedItems)
            {
                var itemView = LoadItemView(_inventoryView.ItemsPoint);
                itemView.Init(item.Info);
                
                _items.Add(itemView.gameObject.GetInstanceID(), item);
            }
        }
        
        private InventoryItemView LoadItemView(Transform spawnPosition)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_itemViewPath), spawnPosition, false);
            AddGameObject(objectView);

            if (!objectView.TryGetComponent<InventoryItemView>(out var inventoryItemView))
                throw new Exception("Компонент InventoryItemView не найден на View объекте!");

            return inventoryItemView;
        }

        protected override void OnDispose()
        {
            _items.Clear();
        }
    }
}
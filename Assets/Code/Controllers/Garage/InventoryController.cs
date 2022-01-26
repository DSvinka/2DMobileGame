using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Configs.Items;
using Code.Interfaces;
using Code.Models;
using Code.Repositories;
using Code.Utils;
using Code.Views;
using Code.Views.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Garage
{
    public sealed class InventoryController: BaseController
    {
        private readonly ResourcePath _itemViewPath = new ResourcePath() { PathResource = "Prefabs/UI/Items/InventoryItem" };

        private readonly InventoryModel _inventoryModel;
        private readonly InventoryView _inventoryView;
        private readonly IRepository<int, IItem> _itemsRepository;

        private readonly Dictionary<int, IItem> _items;

        public InventoryController(GarageView garageView, DataSources dataSources)
        {
            _inventoryView = garageView.Inventory;
            _inventoryModel = new InventoryModel();
            var itemsRepository = new ItemsRepository(dataSources.ItemsDataSource);
            AddController(itemsRepository);

            _itemsRepository = itemsRepository;
            _items = new Dictionary<int, IItem>();
        }

        public void LoadInventory()
        {
            foreach (var item in _itemsRepository.Collection.Values)
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
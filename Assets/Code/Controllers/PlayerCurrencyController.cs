using System;
using Code.Models;
using Code.Types;
using Code.Utils;
using Code.Views.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    public sealed class PlayerCurrencyController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath() { PathResource = "Prefabs/UI/PlayerCurrency" };
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly PlayerCurrencyView _playerCurrencyView;

        public PlayerCurrencyController(Transform placeForUI, PlayerProfileModel playerProfileModel)
        {
            _playerProfileModel = playerProfileModel;
            var saveCurrencyModel = _playerProfileModel.SavesRepository.SaveCurrencyModel;
            saveCurrencyModel.OnCurrencyCountChange += OnCurrencyCountChange;

            _playerCurrencyView = LoadView(placeForUI);
            _playerCurrencyView.Init(saveCurrencyModel.CurrencyWoodCount, saveCurrencyModel.CurrencyMetalCount, saveCurrencyModel.CurrencyMoneyCount);
        }

        protected override void OnDispose()
        {
            _playerProfileModel.SavesRepository.SaveCurrencyModel.OnCurrencyCountChange -= OnCurrencyCountChange;
        }
        
        private PlayerCurrencyView LoadView(Transform spawnUIPosition)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), spawnUIPosition, false);
            AddGameObject(objectView);

            if (!objectView.TryGetComponent<PlayerCurrencyView>(out var playerCurrencyView))
                throw new Exception("Компонент PlayerCurrencyView не найден на View объекте!");

            return playerCurrencyView;
        }

        public void OnCurrencyCountChange(CurrencyType currencyType)
        {
            var saveCurrencyModel = _playerProfileModel.SavesRepository.SaveCurrencyModel;
            switch (currencyType)
            {
                case CurrencyType.Metal:
                    _playerCurrencyView.SetData(currencyType, saveCurrencyModel.CurrencyMetalCount);
                    break;
                
                case CurrencyType.Money:
                    _playerCurrencyView.SetData(currencyType, saveCurrencyModel.CurrencyMoneyCount);
                    break;
                
                case CurrencyType.Wood:
                    _playerCurrencyView.SetData(currencyType, saveCurrencyModel.CurrencyWoodCount);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null);
            }
        }
    }
}
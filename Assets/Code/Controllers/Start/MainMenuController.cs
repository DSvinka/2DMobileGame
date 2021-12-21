using System;
using Code.Models;
using Code.States;
using Code.Utils;
using Code.Views;
using UnityEngine;
using UnityEngine.Purchasing;
using Object = UnityEngine.Object;

namespace Code.Controllers.Start
{
    public sealed class MainMenuController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath() { PathResource = "Prefabs/mainMenu" };
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly PurchaseModel _purchaseModel;
        
        private readonly MainMenuView _mainMenuView;
        private readonly ProductsMenuView _productsMenuView;
        
        public MainMenuController(Transform position, PlayerProfileModel playerProfileModel, PurchaseModel purchaseModel)
        {
            _playerProfileModel = playerProfileModel;
            _purchaseModel = purchaseModel;

            _mainMenuView = LoadView(position);
            _productsMenuView = _mainMenuView.ProductsMenuView;
            
            var carController = new PurchaseController(_productsMenuView, playerProfileModel, purchaseModel);
            AddController(carController);

            _mainMenuView.Init(StartGame, DonateMenu);
        }

        private MainMenuView LoadView(Transform position)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), position, false);
            AddGameObject(objectView);

            if (!objectView.TryGetComponent<MainMenuView>(out var mainMenuView))
                throw new Exception("Компонент MainMenuView не найден на View объекте!");

            return mainMenuView;
        }

        private void StartGame()
        {
            _playerProfileModel.CurrentGameState.Value = GameState.Game;
            _playerProfileModel.AnalyticsTools.SendMessage("startGame", ("time", Time.realtimeSinceStartup));
            _playerProfileModel.AdsShower.ShowBanner();
        }
        
        private void DonateMenu()
        {
            var menuGameObject = _productsMenuView.gameObject;
            
            menuGameObject.SetActive(!menuGameObject.activeSelf);
            _playerProfileModel.AnalyticsTools.SendMessage("donateMenuOpened", ("time", Time.realtimeSinceStartup));
        }
    }
}
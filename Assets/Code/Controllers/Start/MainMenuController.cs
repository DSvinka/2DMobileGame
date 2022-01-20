using System;
using Code.Configs;
using Code.Configs.Rewards;
using Code.Models;
using Code.States;
using Code.Utils;
using Code.Views;
using Code.Views.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Start
{
    public sealed class MainMenuController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath() { PathResource = "Prefabs/UI/Menus/MainMenu" };
        private readonly PlayerProfileModel _playerProfileModel;

        private readonly PurchaseController _purchaseController;
        private readonly DailyRewardsController _dailyRewardsController;

        private readonly MainMenuView _mainMenuView;
        private readonly DailyRewardsView _dailyRewardsView;
        private readonly ProductsMenuView _productsMenuView;
        
        public MainMenuController(Transform spawnUIPosition, PlayerProfileModel playerProfileModel, DataSources dataSources, PurchaseModel purchaseModel)
        {
            _playerProfileModel = playerProfileModel;

            _mainMenuView = LoadView(spawnUIPosition);
            _productsMenuView = _mainMenuView.ProductsMenuView;
            _dailyRewardsView = _mainMenuView.DailyRewardsView;
            
            _purchaseController = new PurchaseController(_mainMenuView.ProductsMenuView, playerProfileModel, purchaseModel);
            AddController(_purchaseController);

            _dailyRewardsController = new DailyRewardsController(_mainMenuView.DailyRewardsView, playerProfileModel, dataSources);
            AddController(_dailyRewardsController);

            _mainMenuView.Init(StartGame, OpenDonateMenu, OpenDailyRewardsMenu);
        }

        private MainMenuView LoadView(Transform spawnUIPosition)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), spawnUIPosition, false);
            AddGameObject(objectView);

            if (!objectView.TryGetComponent<MainMenuView>(out var mainMenuView))
                throw new Exception("Компонент MainMenuView не найден на View объекте!");

            return mainMenuView;
        }

        private void StartGame()
        {
            _playerProfileModel.CurrentGameState.Value = GameState.Game;
            _playerProfileModel.AnalyticsTools.SendMessage("startGame", ("time", Time.realtimeSinceStartup));
            // TODO: Отключил так как пока что нету интернета
            //_playerProfileModel.AdsShower.ShowBanner();
        }
        
        private void OpenDonateMenu()
        {
            _purchaseController.OpenMenu();
            _playerProfileModel.AnalyticsTools.SendMessage("donateMenuOpened", ("time", Time.realtimeSinceStartup));
        }
        
        private void OpenDailyRewardsMenu()
        {
            _dailyRewardsController.OpenMenu();
            _playerProfileModel.AnalyticsTools.SendMessage("dailyRewardsMenuOpened", ("time", Time.realtimeSinceStartup));
        }
    }
}
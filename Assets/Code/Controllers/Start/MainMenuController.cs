using System;
using Code.Configs;
using Code.Enums;
using Code.Models;
using Code.Utils;
using Code.Views.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Start
{
    public sealed class MainMenuController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath() { PathResource = "Prefabs/UI/Menus/MainMenu" };
        private readonly PlayerProfileModel _playerProfileModel;
        
        private readonly DailyRewardsController _dailyRewardsController;

        private readonly MainMenuView _mainMenuView;

        public MainMenuController(Transform spawnUIPosition, PlayerProfileModel playerProfileModel, DataSources dataSources)
        {
            _playerProfileModel = playerProfileModel;

            _mainMenuView = LoadView(spawnUIPosition);

            _dailyRewardsController = new DailyRewardsController(_mainMenuView.DailyRewardsView, playerProfileModel, dataSources);
            AddController(_dailyRewardsController);

            _mainMenuView.Init(StartGame, ExitGame, OpenDailyRewardsMenu);
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
        
        private void ExitGame()
        {
            Application.Quit();
        }

        private void OpenDailyRewardsMenu()
        {
            _dailyRewardsController.OpenMenu();
            _playerProfileModel.AnalyticsTools.SendMessage("dailyRewardsMenuOpened", ("time", Time.realtimeSinceStartup));
        }
    }
}
using System;
using Code.Models;
using Code.States;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    public sealed class MainMenuController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath() { PathResource = "Prefabs/mainMenu" };
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly MainMenuView _mainMenuView;

        public MainMenuController(Transform position, PlayerProfileModel playerProfileModel)
        {
            _playerProfileModel = playerProfileModel;
            _mainMenuView = LoadView(position);
            _mainMenuView.Init(StartGame);
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
        }
    }
}
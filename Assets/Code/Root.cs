using Code.Analytics;
using Code.Configs;
using Code.Controllers;
using Code.Enums;
using Code.Models;
using UnityEngine;

namespace Code
{
    public sealed class Root : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _uiPoint;
        [SerializeField] private UnityAdsTools _unityAdsTools;
        [SerializeField] private UnityPurchasingTools _unityPurchasingTools;
        
        [SerializeField] private DataSources _dataSources;

        private MainController _mainController;

        private void Awake()
        {
            var playerProfileModel = new PlayerProfileModel(15f, _dataSources, _unityAdsTools, _unityPurchasingTools);
            playerProfileModel.CurrentGameState.Value = GameState.Start;
            
            _mainController = new MainController(_uiPoint, playerProfileModel, _dataSources, _camera);
        }

        private void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}

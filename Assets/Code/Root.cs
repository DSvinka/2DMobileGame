using Code.Analytics;
using Code.Controllers;
using Code.Models;
using Code.States;
using UnityEngine;

namespace Code
{
    public sealed class Root : MonoBehaviour
    {
        [SerializeField] private Transform _uiPoint;
        [SerializeField] private UnityAdsTools _unityAdsTools;
        
        private MainController _mainController;

        private void Awake()
        {
            var playerProfile = new PlayerProfileModel(15f, _unityAdsTools);
            playerProfile.CurrentGameState.Value = GameState.Start;
            _mainController = new MainController(_uiPoint, playerProfile);
        }

        private void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views
{
    public sealed class MainMenuView: MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;

        public void Init(UnityAction startGame)
        {
            _buttonStart.onClick.AddListener(startGame);
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
        }
    }
}
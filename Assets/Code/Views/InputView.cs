using Code.Models;
using Code.Properties;
using JoostenProductions;
using UnityEngine;

namespace Code.Views
{
    public sealed class InputView : BaseInputView
    {
        private Camera _camera;
        
        public override void Init(InputModel inputModel, float speed)
        {
            base.Init(inputModel, speed);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

        private void Move()
        {
            var direction = Vector3.zero;

            // TODO: Временное управление для тестирование на ПК.
            var position = Input.mousePosition; // Input.GetTouch(0).position;

            if (direction.sqrMagnitude > 1)
                direction.Normalize();
            
            OnTouchMove(position);
            OnMove(Time.deltaTime);
        }
    }
}


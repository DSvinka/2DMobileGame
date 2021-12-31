using Code.Properties;
using JoostenProductions;
using UnityEngine;

namespace Code.Views
{
    public sealed class InputAccelerationView : BaseInputView
    {
        public override void Init(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

        private void Move()
        {
            var direction = Vector3.zero; 
            
            // TODO: Временный Костыль
            direction.x = -Input.GetAxis("Horizontal"); // -Input.acceleration.y;
            direction.z = Input.GetAxis("Vertical"); // Input.acceleration.x;
        
            if (direction.sqrMagnitude > 1)
                direction.Normalize();

            // TODO: Временный Костыль
            OnRightMove(direction.sqrMagnitude / 20 * _speed);
        }
    }
}


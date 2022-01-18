using Code.Models;
using Code.Properties;
using UnityEngine;

namespace Code
{
    public abstract class BaseInputView : MonoBehaviour
    {
        protected float _speed;
        private SubscribeProperty<float> _moveUpdate;
        private SubscribeProperty<Vector2> _touchPosition;

        public virtual void Init(InputModel inputModel, float speed)
        {
            _touchPosition = inputModel.TouchPosition;
            _moveUpdate = inputModel.MoveUpdate;
            _speed = speed;
        }

        protected virtual void OnTouchMove(Vector2 position)
        {
            _touchPosition.Value = position;
        }
        
        protected virtual void OnMove(float deltatime)
        {
            _moveUpdate.Value = deltatime;
        }
    } 

}
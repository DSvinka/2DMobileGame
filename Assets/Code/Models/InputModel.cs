using Code.Properties;
using UnityEngine;

namespace Code.Models
{
    public struct InputModel
    {
        private SubscribeProperty<Vector2> _touchPosition;
        private SubscribeProperty<float> _moveUpdate;
        
        public SubscribeProperty<Vector2> TouchPosition => _touchPosition;
        public SubscribeProperty<float> MoveUpdate => _moveUpdate; 

        public InputModel(SubscribeProperty<Vector2> touchPosition, SubscribeProperty<float> moveUpdate)
        {
            _touchPosition = touchPosition;
            _moveUpdate = moveUpdate;
        }
    }
}
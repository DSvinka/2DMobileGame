using Code.Interfaces;
using System;
using Code.Interfaces.Properties;

namespace Code.Properties
{
    public sealed class SubscribeProperty<T> : IReadOnlySubscribeProperty<T>
    {
        private T _value;
        private Action<T> _onChangeValue;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onChangeValue?.Invoke(_value);
            }
        }
        
        public void SubscribeOnChange(Action<T> subscriptionAction)
        {
            _onChangeValue += subscriptionAction;
        }

        public void UnSubscribeOnChange(Action<T> unSubscriptionAction)
        {
            _onChangeValue -= unSubscriptionAction;
        }
    }
}
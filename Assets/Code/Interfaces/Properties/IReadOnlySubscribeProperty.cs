using System;

namespace Code.Interfaces.Properties
{
    public interface IReadOnlySubscribeProperty<T>
    {
        T Value { get; }
        void SubscribeOnChange(Action<T> subscriptionAction);
        void UnSubscribeOnChange(Action<T> unSubscriptionAction);
    }
}
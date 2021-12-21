using System;
using UnityEngine.Purchasing;

namespace Code.Analytics
{
    public interface IUnityPurchasingTools
    {
        event Action<Product> OnPurchase;
        ConfigurationBuilder GetBuilder();

        void Init(ConfigurationBuilder builder);
        void BuyProduct(string productID);
    }
}
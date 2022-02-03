using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Code.UnityUtils
{
    public class UnityPurchasingTools : MonoBehaviour, IStoreListener, IUnityPurchasingTools
    {
        private IStoreController _storeController;

        public event Action<Product> OnPurchase = delegate(Product product) {  };

        public ConfigurationBuilder GetBuilder()
        {
            return ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        }

        public void Init(ConfigurationBuilder builder)
        {
            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyProduct(string productID)
        {
            _storeController.InitiatePurchase(productID);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log($"Произошла ошибка в работе IAP: {error}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            var product = args.purchasedProduct;
            OnPurchase.Invoke(product);

            Debug.Log($"Оплата прошла: {product.definition.id}");
            
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Оплата провалена: '{product.definition.id}', Причина: {failureReason}");
        }
    }
}
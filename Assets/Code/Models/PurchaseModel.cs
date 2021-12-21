using UnityEngine.Purchasing;

namespace Code.Models
{
    public sealed class PurchaseModel
    {
        public ProductConf[] Products { get; }

        public PurchaseModel(ProductConf[] products)
        {
            Products = products;
        }
    }

    public struct ProductConf
    {
        public string ID { get; }
        public string Title { get; }
        public string Description { get; }
        public int PriceRubles { get; }
        public ProductType Type { get; }

        public ProductConf(string id, string title, string description, int priceRubles, ProductType productType)
        {
            ID = id;
            Title = title;
            Description = description;
            PriceRubles = priceRubles;
            Type = productType;
        }
    }
}
using UnityEngine;

namespace Code.Views
{
    public sealed class InventoryView: MonoBehaviour
    {
        [SerializeField] private Transform _itemsPoint;

        public Transform ItemsPoint => _itemsPoint;
    }
}
using UnityEngine;

namespace Code.Configs.Items
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/Items/ItemConfig")]
    public sealed class ItemConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private ItemInfo _itemInfo;

        public int ID => _id;
        public ItemInfo ItemInfo => _itemInfo;
    }
}
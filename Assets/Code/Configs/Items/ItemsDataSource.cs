using UnityEngine;

namespace Code.Configs.Items
{
    [CreateAssetMenu(fileName = "ItemsDataSource", menuName = "Configs/Items/DataSource", order = -1)]
    public sealed class ItemsDataSource : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _itemConfigs;

        public ItemConfig[] ItemConfigs => _itemConfigs;
    }
}
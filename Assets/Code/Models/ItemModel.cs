using Code.Configs.Items;
using Code.Interfaces;

namespace Code.Models
{
    public sealed class ItemModel: IItem
    {
        public int ID { get; }
        public ItemInfo Info { get; }

        public ItemModel(int id, ItemInfo info)
        {
            ID = id;
            Info = info;
        }
    }
}
using Code.Configs.Items;

namespace Code.Interfaces.Models
{
    public interface IItemModel
    {
        int ID { get; }
        ItemInfo Info { get; }
    }
}
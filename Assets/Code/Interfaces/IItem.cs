using Code.Configs.Items;

namespace Code.Interfaces
{
    public interface IItem
    {
        int ID { get; }
        ItemInfo Info { get; }
    }
}
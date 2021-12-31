using System.Collections.Generic;

namespace Code.Interfaces.Repositories
{
    public interface IItemsRepository
    {
        IReadOnlyDictionary<int, IItem> Items { get; }
    }
}
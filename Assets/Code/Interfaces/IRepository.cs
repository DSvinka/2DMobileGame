using System.Collections.Generic;

namespace Code.Interfaces
{
    public interface IRepository<TKey, TValue>
    {
        IReadOnlyDictionary<TKey, TValue> Collection { get; }
    }
}
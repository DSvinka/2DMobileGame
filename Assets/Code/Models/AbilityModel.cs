using System.Collections.Generic;
using Code.Interfaces;

namespace Code.Models
{
    public sealed class AbilityModel
    {
        private readonly List<IAbility> _abilities = new List<IAbility>();
        
        public IReadOnlyList<IAbility> GetEquippedItems()
        {
            return _abilities;
        }

        public void EquipItem(IAbility item)
        {
            if (_abilities.Contains(item))
                return;
            
            _abilities.Add(item);
        }
        
        public void UnEquipItem(IAbility item)
        {
            if (!_abilities.Contains(item))
                return;
            
            _abilities.Remove(item);
        }
    }
}
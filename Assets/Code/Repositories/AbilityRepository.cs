using System;
using System.Collections.Generic;
using Code.Configs.Abilities;
using Code.Configs.Items;
using Code.Interfaces;
using Code.Models;
using UnityEngine;

namespace Code.Repositories
{
    public sealed class AbilityRepository: BaseController, IRepository<int, IAbility>
    {
        public IReadOnlyDictionary<int, IAbility> Collection => _abilityMapById;
        private Dictionary<int, IAbility> _abilityMapById;
        
        public AbilityRepository(List<AbilityItemConfig> configs)
        {
            _abilityMapById = new Dictionary<int, IAbility>();
            PopulateItems(configs);
        }

        private void PopulateItems(List<AbilityItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (_abilityMapById.ContainsKey(config.ID))
                    continue;
                
                _abilityMapById.Add(config.ID, createAbility(config));
            }
        }

        private IAbility createAbility(AbilityItemConfig config)
        {
            switch (config.AbilityType)
            {
                case AbilityType.Gun:
                    return new BombAbility(config);

                default:
                    Debug.LogError($"AbilityType {config.AbilityType} не предусмотрен!");
                    return null;
            }
        }

        protected override void OnDispose()
        {
            _abilityMapById.Clear();
        }
    }
}
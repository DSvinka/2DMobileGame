using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs.Abilities;
using Code.Configs.Items;
using Code.Interfaces;
using Code.Models;
using Code.Repositories;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    public sealed class AbilityController: BaseController
    {
        private readonly AbilityCollectionView _abilityCollectionView;
        private readonly IRepository<int, IAbility> _itemsRepository;

        public AbilityController(List<AbilityItemConfig> configs, HudView hudView)
        {
            _abilityCollectionView = hudView.AbilityMenu;
            var itemsRepository = new AbilityRepository(configs);
            AddController(itemsRepository);

            _itemsRepository = itemsRepository;
            LoadAbilities();
        }

        private void UseAbility(IAbility ability)
        {
            ability.Apply();
        }

        public void LoadAbilities()
        {
            _abilityCollectionView.UseRequested += UseAbility;
            
            _abilityCollectionView.Init(_itemsRepository.Collection.Values.ToList());
            _abilityCollectionView.Show();
        }
    }
}
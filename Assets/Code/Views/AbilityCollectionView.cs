using System;
using System.Collections.Generic;
using Code.Interfaces;
using Code.Interfaces.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views
{
    public sealed class AbilityCollectionView: MonoBehaviour, IAbilityCollectionView
    {
        [SerializeField] private AbilityCollectionItemView _abilityCollectionItemView;
        [SerializeField] private Transform _itemsPoint;
        
        private List<AbilityCollectionItemView> _abilityCollectionItemViews;
        private IReadOnlyList<IAbility> _abilityItems;

        public event Action<IAbility> UseRequested;
        private void OnUseRequested(IAbility item)
        {
            UseRequested?.Invoke(item);
        }

        private void OnDestroy()
        {
            _abilityCollectionItemViews.Clear();
        }

        public void Init(IReadOnlyList<IAbility> abilityItems)
        {
            _abilityCollectionItemViews = new List<AbilityCollectionItemView>();
            _abilityItems = abilityItems;
        }

        public void Show()
        {
            foreach (var abilityItem in _abilityItems)
            {
                var itemView = Instantiate(_abilityCollectionItemView, _itemsPoint);
                itemView.Init(delegate { OnUseRequested(abilityItem); }, abilityItem.AbilityItemConfig.ItemConfig.Icon);
                _abilityCollectionItemViews.Add(itemView);
            }
        }

        public void Hide()
        {
            foreach (var abilityItem in _abilityCollectionItemViews)
            {
                Destroy(abilityItem);
            }
            _abilityCollectionItemViews.Clear();
        }
    }
}
using Code.Configs.Items;
using Code.Views;
using UnityEngine;

namespace Code.Configs.Abilities
{
    [CreateAssetMenu(fileName = "AbilityItemConfig", menuName = "Configs/AbilityItemConfig")]
    public sealed class AbilityItemConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig _itemConfig;
        [SerializeField] private AbilityView _view;
        [SerializeField] private AbilityType _abilityType;
        [SerializeField] private float _value;

        public int ID => ItemConfig.ID;
        
        public ItemConfig ItemConfig => _itemConfig;
        public AbilityView View => _view;
        public AbilityType AbilityType => _abilityType;
        public float Value => _value;
    }

    public enum AbilityType
    {
        None = 0,
        Gun = 1,
    }
}
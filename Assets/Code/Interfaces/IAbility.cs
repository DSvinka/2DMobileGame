using Code.Configs.Abilities;

namespace Code.Interfaces
{
    public interface IAbility
    {
        public AbilityItemConfig AbilityItemConfig { get; }
        
        void Apply();
    }
}
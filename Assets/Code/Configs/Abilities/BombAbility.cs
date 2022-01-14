using System;
using Code.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Configs.Abilities
{
    public sealed class BombAbility: IAbility
    {
        private readonly AbilityItemConfig _config;
        public AbilityItemConfig AbilityItemConfig => _config;

        public BombAbility(AbilityItemConfig config)
        {
            _config = config;
        }

        public void Apply()
        {
            var bomb = Object.Instantiate(_config.View);
            bomb.Rigidbody.AddForce(Vector2.right * _config.Value, ForceMode2D.Impulse);
        }
    }
}
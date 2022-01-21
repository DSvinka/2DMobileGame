using System;
using Code.Enums;
using TMPro;
using UnityEngine;

namespace Code.Views
{
    public sealed class PlayerCarView: EntityView
    {
        [SerializeField] private TurretView _turretView;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private GameObject[] _wheels;
        private EntityType _entityType;
        
        public override event Action<int, float> OnDamage;
        public override EntityType EntityType => _entityType;
        
        public TurretView TurretView => _turretView;

        public override void Init(EntityType entityType)
        {
            _entityType = entityType;
        }

        public override void AddDamage(float damage)
        {
            OnDamage?.Invoke(gameObject.GetInstanceID(), damage);
        }

        public void UpdateHealthDisplay(float health)
        {
            _healthText.text = $"HP: {health}";
        }

        public void RotateWheels(Vector3 value)
        {
            for (var i = 0; i < _wheels.Length; i++)
            {
                _wheels[i].transform.Rotate(value);
            }
        }
    }
}
using System;
using Code.Enums;
using TMPro;
using UnityEngine;

namespace Code.Views
{
    public sealed class EnemyView: EntityView
    {
        [SerializeField] private TurretView _turretView;
        [SerializeField] private TMP_Text _healthText;
        private EntityType _entityType;
        
        public override event Action<int, float> OnDamage;
        public override EntityType EntityType => _entityType;
        public TurretView TurretView => _turretView;
        public TMP_Text HealthText => _healthText;

        public override void Init(EntityType entityType)
        {
            _entityType = entityType;
        }

        public override void AddDamage(float damage)
        {
            OnDamage?.Invoke(gameObject.GetInstanceID(), damage);
        }
    }
}
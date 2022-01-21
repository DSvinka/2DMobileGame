using System;
using Code.Enums;
using UnityEngine;

namespace Code.Views
{
    public abstract class EntityView: MonoBehaviour
    {
        public abstract EntityType EntityType { get; }

        public abstract event Action<int, float> OnDamage;
        
        public abstract void Init(EntityType entityType);
        public abstract void AddDamage(float damage);
    }
}
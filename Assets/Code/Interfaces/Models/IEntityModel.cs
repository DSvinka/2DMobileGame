using System;
using UnityEngine;

namespace Code.Interfaces.Models
{
    public interface IEntityModel<T>
    {
        public T EntityView { get; }
        public GameObject GameObject { get; }
        public Transform Transform { get; }

        public int ID { get; }

        public float MaxHealth { get; }
        public float ShotRate { get; }

        public float BulletLifeTime { get; }
        public float BulletShotForce { get; }
        public float BulletDamage { get; }

        public float Health { get; }
        public float ShotCooldown { get; }

        public event Action<int> OnDeath;
        public event Action<int, float> OnDamage;

        public void SetEntityView(T entityView);
        public void AddDamage(float damage);

        public bool CheckCooldown();
        public void ReduceCooldown(float count);
        public void ResetCooldown();
    }

    public interface IEntityModelConfig<T>
    {
        public T EntityView { get; set; }

        public float MaxHealth { get; set; }
        public float ShotRate { get; set; }

        public float BulletLifeTime { get; set; }
        public float BulletShotForce { get; set; }
        public float BulletDamage { get; set; }
    }
}
using System;
using Code.Enums;
using UnityEngine;

namespace Code.Views
{
    public sealed class BulletView: MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        public Rigidbody2D Rigidbody => _rigidbody;

        private EntityType _ownerEntityType;
        private float _damage;

        public void Init(float damage, EntityType ownerEntityType)
        {
            _damage = damage;
            _ownerEntityType = ownerEntityType;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<EntityView>(out var entityView))
            {
                if (entityView.EntityType == _ownerEntityType) 
                    return;
                
                entityView.AddDamage(_damage);
            }
            
            Destroy(gameObject);
        }
    }
}
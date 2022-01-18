using System;
using UnityEngine;

namespace Code.Views
{
    public sealed class TurretView: MonoBehaviour
    {
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private Transform _gun;

        public Transform ShotPoint => _shotPoint;
        public Transform Gun => _gun;
        
        public event Action<int> OnShotRequest;
        
        private float _shotRate;
        private float _shotCooldown;

        public void Init(float shotRate)
        {
            _shotRate = shotRate;
            _shotCooldown = shotRate;
        }

        private void Update()
        {
            _shotCooldown -= Time.deltaTime;
            if (_shotCooldown <= 0)
            {
                _shotCooldown = _shotRate;
                OnShotRequest?.Invoke(gameObject.GetInstanceID());
            }
        }
    }
}
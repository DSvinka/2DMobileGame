using UnityEngine;

namespace Code.Configs.Settings
{
    [CreateAssetMenu(fileName = "SettingsPlayerConfig", menuName = "Configs/Settings/SettingsPlayerConfig")]
    public sealed class SettingsPlayerConfig: ScriptableObject
    {
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _damage = 15f;
            
        [SerializeField] private float _bulletShotRate = 1f;
        [SerializeField] private float _bulletShotForce = 15f;
        [SerializeField] private float _bulletLifeTime = 20f;

        public float Health => _health;
        public float Damage => _damage;

        public float BulletShotRate => _bulletShotRate;
        public float BulletShotForce => _bulletShotForce;
        public float BulletLifeTime => _bulletLifeTime;
    }
}
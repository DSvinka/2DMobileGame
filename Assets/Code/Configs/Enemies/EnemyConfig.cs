using Code.Enums;
using Code.Views;
using UnityEngine;

namespace Code.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemies/EnemyConfig")]
    public sealed class EnemyConfig: ScriptableObject
    {
        [SerializeField] private EnemyType _enemyType = EnemyType.Car;
        [SerializeField] private EnemyView _enemyViewPrefab;

        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _shotRate = 2f;
        [SerializeField] private int _giveMoneyOnDeath = 35;
        [SerializeField] private int _maxSpawnCount = 2;
        
        [SerializeField] private float _bulletLifeTime = 20f;
        [SerializeField] private float _bulletShotForce = 15f;
        [SerializeField] private float _bulletDamage = 15f;

        
        public EnemyType EnemyType => _enemyType;
        public EnemyView EnemyView => _enemyViewPrefab;

        public float MaxHealth => _maxHealth;
        public float ShotRate => _shotRate;
        public int GiveMoneyOnDeath => _giveMoneyOnDeath;
        public int MaxSpawnCount => _maxSpawnCount;

        public float BulletLifeTime => _bulletLifeTime;
        public float BulletShotForce => _bulletShotForce;
        public float BulletDamage => _bulletDamage;
    }
}
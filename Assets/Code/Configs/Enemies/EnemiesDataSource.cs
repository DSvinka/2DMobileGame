using UnityEngine;

namespace Code.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemiesDataSource", menuName = "Configs/Enemies/DataSource", order = -1)]
    public sealed class EnemiesDataSource: ScriptableObject
    {
        [SerializeField] private EnemyConfig[] _enemyConfigs;

        public EnemyConfig[] EnemyConfigs => _enemyConfigs;
    }
}
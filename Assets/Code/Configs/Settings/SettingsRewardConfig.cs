using UnityEngine;

namespace Code.Configs.Settings
{
    [CreateAssetMenu(fileName = "SettingsRewardConfig", menuName = "Configs/Settings/SettingsRewardConfig")]
    public sealed class SettingsRewardConfig : ScriptableObject
    {
        [SerializeField] private float _timeCooldown = 86400;
        [SerializeField] private float _timeDeadline = 172800;

        public float TimeCooldown => _timeCooldown;
        public float TimeDeadLine => _timeDeadline;
    }
}
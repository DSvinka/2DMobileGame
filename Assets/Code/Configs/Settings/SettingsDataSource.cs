using Code.Configs.Settings;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "SettingsDataSource", menuName = "Configs/Settings/DataSource", order = -1)]
    public sealed class SettingsDataSource : ScriptableObject
    {
        [SerializeField] private SettingsRewardConfig _settingsRewardConfig;
        [SerializeField] private SettingsPlayerConfig _settingsPlayerConfig;

        public SettingsRewardConfig SettingsRewardConfig => _settingsRewardConfig;
        public SettingsPlayerConfig SettingsPlayerConfig => _settingsPlayerConfig;
    }
}
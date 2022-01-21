using Code.Configs.Settings;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "SettingsDataSource", menuName = "Configs/Settings/DataSource", order = -1)]
    public class SettingsDataSource : ScriptableObject
    {
        [SerializeField] private SettingsRewardConfig _settingsRewardConfig;

        public SettingsRewardConfig SettingsRewardConfig => _settingsRewardConfig;
    }
}
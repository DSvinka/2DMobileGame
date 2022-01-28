using Code.Repositories.Models;
using UnityEngine;

namespace Code.Repositories
{
    public sealed class SavesRepository: BaseController
    {
        private readonly RewardSaveModel _rewardSaveModel;
        private readonly CurrencySaveModel _currencySaveModel;
        private readonly UpgradesSaveModel _upgradesSaveModel;
        
        public RewardSaveModel RewardSaveModel => _rewardSaveModel;
        public CurrencySaveModel CurrencySaveModel => _currencySaveModel;
        public UpgradesSaveModel UpgradesSaveModel => _upgradesSaveModel;
        
        public SavesRepository()
        {
            _rewardSaveModel = new RewardSaveModel();
            _currencySaveModel = new CurrencySaveModel();
            _upgradesSaveModel = new UpgradesSaveModel();
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
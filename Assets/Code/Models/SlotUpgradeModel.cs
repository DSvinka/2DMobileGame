using Code.Interfaces.Models;
using Code.Views.UI;

namespace Code.Models
{
    public sealed class SlotUpgradeModel
    {
        private SlotUpgradeView _slotUpgradeView;
        private IUpgradeModel _upgradeModel;

        public SlotUpgradeView SlotUpgradeView => _slotUpgradeView;
        public IUpgradeModel UpgradeModel => _upgradeModel;

        public SlotUpgradeModel(SlotUpgradeView slotUpgradeView, IUpgradeModel upgradeModel)
        {
            _slotUpgradeView = slotUpgradeView;
            _upgradeModel = upgradeModel;
        }
    }
}
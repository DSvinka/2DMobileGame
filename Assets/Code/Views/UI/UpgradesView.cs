using UnityEngine;

namespace Code.Views.UI
{
    public sealed class UpgradesView: MonoBehaviour
    {
        [SerializeField] private SlotUpgradeView _slotUpgradeViewPrefab;
        [SerializeField] private Transform _gridSlotsUpgrades;

        public Transform GridSlotsUpgrades => _gridSlotsUpgrades;
        public SlotUpgradeView SlotUpgradeViewPrefab => _slotUpgradeViewPrefab;
    }
}
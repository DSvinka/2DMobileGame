using TMPro;
using UnityEngine;

namespace Code.Views.UI
{
    public sealed class HudView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _distanceToGarage;

        public void ChangeDistanceToGarage(float distance)
        {
            _distanceToGarage.text = $"Гараж: {distance}";
        }
    }
}
using TMPro;
using UnityEngine;

namespace Code.Views.UI
{
    public sealed class HudView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _distanceToGarage;

        public void ChangeDistanceToGarageText(string text)
        {
            _distanceToGarage.text = text;
        }
    }
}
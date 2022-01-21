using Code.Interfaces.Properties;
using UnityEngine;

namespace Code.Views
{
    public sealed class TapeBackgroundView: MonoBehaviour
    {
        [SerializeField] private BackgroundView[] _backgrounds;

        private IReadOnlySubscribeProperty<float> _diff;

        public void Init(IReadOnlySubscribeProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscribeOnChange(Move);
        }

        private void OnDestroy()
        {
            _diff?.SubscribeOnChange(Move);
        }

        private void Move(float value)
        {
            foreach (var background in _backgrounds)
                background.Move(-value);
        }
    }
}
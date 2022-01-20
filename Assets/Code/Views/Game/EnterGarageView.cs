using Code.Interfaces.Properties;
using UnityEngine;

namespace Code.Views
{
    public sealed class EnterGarageView: MonoBehaviour
    {
        [SerializeField] private float _relativeSpeedRate = 0.3f;
        
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
            transform.position += Vector3.right * -value * _relativeSpeedRate;
        }
    }
}
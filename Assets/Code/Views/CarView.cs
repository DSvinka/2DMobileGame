using UnityEngine;

namespace Code.Views
{
    public sealed class CarView: MonoBehaviour
    {
        [SerializeField] private GameObject[] _wheels;
        
        public void RotateWheels(Vector3 value)
        {
            for (var i = 0; i < _wheels.Length; i++)
            {
                _wheels[i].transform.Rotate(value);
            }
        }
    }
}
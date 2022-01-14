using System;
using Code.Interfaces.Views;
using UnityEngine;

namespace Code.Views
{
    public sealed class AbilityView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        public Rigidbody2D Rigidbody => _rigidbody;
    }
}
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Views.UI.CustomElements.Buttons
{
    public sealed class AnimateButton: Button
    {
        public static string ChangeButtonType => nameof(_animationButtonType);
        public static string Duration => nameof(_duration);
        public static string Strength => nameof(_strength);

        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _strength = 30f;
        
        private RectTransform _rectTransform;
        private bool _isSelected;
        private bool _isMove;

        protected override void Awake()
        {
            base.Awake();
            _rectTransform = GetComponent<RectTransform>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (!_isMove)
                ActivateAnimation();
        }

        private void ActivateAnimation()
        {
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangePosition:
                    _isMove = true;
                    _rectTransform.DOShakeAnchorPos(_duration, _strength).OnComplete(() => _isMove = false);
                    break;
                case AnimationButtonType.ChangeRotation:
                    _isMove = true;
                    _rectTransform.DOShakeRotation(_duration, _strength).OnComplete(() => _isMove = false);;
                    break;
                case AnimationButtonType.ChangeScale:
                    _isMove = true;
                    _rectTransform.DOShakeScale(_duration, _strength).OnComplete(() => _isMove = false);;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_animationButtonType), _animationButtonType, null);
            }
        }
    }

    public enum AnimationButtonType
    {
        ChangePosition = 0,
        ChangeRotation = 1,     
        ChangeScale = 2,
    }
}
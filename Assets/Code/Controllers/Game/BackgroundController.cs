using Code.Interfaces.Properties;
using Code.Properties;
using Code.Utils;
using Code.Views;
using UnityEngine;

namespace Code.Controllers.Game
{
    public sealed class BackgroundController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/BackgroundView"};
        private readonly SubscribeProperty<float> _diff;
        private readonly IReadOnlySubscribeProperty<float> _leftMove;
        private readonly IReadOnlySubscribeProperty<float> _rightMove;
        
        private TapeBackgroundView _view;
        
        public BackgroundController(IReadOnlySubscribeProperty<float> leftMove, IReadOnlySubscribeProperty<float> rightMove)
        {
            _view = LoadView();
            _diff = new SubscribeProperty<float>();
        
            _leftMove = leftMove;
            _rightMove = rightMove;
        
            _view.Init(_diff);
        
            _leftMove.SubscribeOnChange(Move);
            _rightMove.SubscribeOnChange(Move);
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscribeOnChange(Move);
            _rightMove.UnSubscribeOnChange(Move);
        
            base.OnDispose();
        }

        private TapeBackgroundView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objView);
        
            return objView.GetComponent<TapeBackgroundView>();
        }

        private void Move(float value)
        {
            _diff.Value = value;
        }
    }
}


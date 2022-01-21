using Code.Views.UI.CustomElements.Buttons;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Code.Views.UI.Editor
{
    [CustomEditor(typeof(AnimateButton))]
    public sealed class AnimateButtonEditor: ButtonEditor
    {
        private SerializedProperty _interactableProperty;

        protected override void OnEnable()
        {
            _interactableProperty = serializedObject.FindProperty("m_Interactable");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            
            var changeButtonType = new PropertyField(serializedObject.FindProperty(AnimateButton.ChangeButtonType));
            var duration = new PropertyField(serializedObject.FindProperty(AnimateButton.Duration));
            var strength = new PropertyField(serializedObject.FindProperty(AnimateButton.Strength));

            var tweenLabel = new Label("Tweens");
            var interactableLabel = new Label("Standart Settings");
            
            root.Add(tweenLabel);
            root.Add(changeButtonType);
            root.Add(duration);
            root.Add(strength);
            
            root.Add(interactableLabel);
            root.Add(new IMGUIContainer(OnInspectorGUI));
            
            return root;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_interactableProperty);
            EditorGUI.BeginChangeCheck();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
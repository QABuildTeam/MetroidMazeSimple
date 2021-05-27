using Utility.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using MetroidMaze.Character;

namespace MetroidMaze.Editor
{
    [CustomPropertyDrawer(typeof(AnimationStateName))]
    public class AnimationStateNamePropertyDrawer : PropertyDrawer
    {
        protected List<string> AnimationNames(AnimatorController animatorController)
        {
            List<string> animationNames = new List<string>();
            if (animatorController != null)
            {
                var layers = animatorController.layers;
                animationNames = layers[0].stateMachine.states.Select(s => s.state.name).ToList();
            }
            return animationNames;
        }

        private void StringPopup(Rect position, SerializedProperty property, List<string> options)
        {
            int selectedIndex = options.IndexOf(property.stringValue);
            selectedIndex = EditorGUI.Popup(position, selectedIndex, options.ToArray());
            property.stringValue = selectedIndex >= 0 && selectedIndex < options.Count ? options[selectedIndex] : string.Empty;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // first line for label
            float totalHeight = EditorGUIUtility.singleLineHeight;

            // second line for animatorController
            SerializedProperty animatorControllerProperty = property.FindPropertyRelative(nameof(AnimationStateName.animatorController));
            totalHeight += EditorGUI.GetPropertyHeight(animatorControllerProperty, label, true) + EditorGUIUtility.standardVerticalSpacing;

            // third line for popup
            totalHeight += EditorGUIUtility.singleLineHeight;

            // plus spacings between them
            totalHeight += EditorGUIUtility.standardVerticalSpacing * 3;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float verticalGap = position.height / 3;
            float height = verticalGap - EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.PrefixLabel(new Rect(position.x, position.y, position.width, height), label);

            int oldIndentlevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = oldIndentlevel + 1;

            SerializedProperty animatorControllerProperty = property.FindPropertyRelative(nameof(AnimationStateName.animatorController));
            EditorGUI.PropertyField(new Rect(position.x, position.y + verticalGap, position.width, height), animatorControllerProperty);

            AnimatorController animatorController = animatorControllerProperty.objectReferenceValue as AnimatorController;
            SerializedProperty nameProperty = property.FindPropertyRelative(nameof(AnimationStateName.name));
            EditorGUI.PrefixLabel(new Rect(position.x, position.y + verticalGap * 2, position.width / 3, height), new GUIContent(nameProperty.displayName));
            StringPopup(new Rect(position.x + position.width / 3, position.y + verticalGap * 2, position.width * 2 / 3, height), nameProperty, AnimationNames(animatorController));

            EditorGUI.indentLevel = oldIndentlevel;
            EditorGUI.EndProperty();
        }
    }
}

using UnityEngine;
using UnityEngine.UIElements;

namespace Obsidize.BehaviourTrees.Editor
{

    [CreateAssetMenu(menuName = "Behaviour Trees/Editor Settings")]
    public class BehaviourTreeEditorSettings : ScriptableObject
    {

        [SerializeField]
        private VisualTreeAsset _editorVisualTree;

        public VisualTreeAsset EditorVisualTree => _editorVisualTree;
    }
}


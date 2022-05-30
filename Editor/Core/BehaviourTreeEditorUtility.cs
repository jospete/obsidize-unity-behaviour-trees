using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Obsidize.BehaviourTrees.Editor
{
    public static class BehaviourTreeEditorUtility
    {

        private const string assetsRoot = "Assets/BehaviourTrees/Editor/Core";
        private const string treeEditorAssetRoot = assetsRoot + "/BehaviourTreeEditor";
        private const string nodeEditorAssetRoot = assetsRoot + "/BehaviourTreeNodeView";
        private const string editorUxmlPath = treeEditorAssetRoot + "/BehaviourTreeEditor.uxml";
        private const string editorStyleSheetPath = treeEditorAssetRoot + "/BehaviourTreeEditor.uss";

        public static string nodeViewXmlPath => nodeEditorAssetRoot + "/BehaviourTreeNodeView.uxml";

        public static readonly NodeState[] ALL_NODE_STATES = Enum.GetValues(typeof(NodeState)).Cast<NodeState>().ToArray();
        public static VisualTreeAsset GetVisualTreeAsset() => AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(editorUxmlPath);
        public static StyleSheet GetStyleSheet() => AssetDatabase.LoadAssetAtPath<StyleSheet>(editorStyleSheetPath);
        public static string GetGraphViewClassName(this NodeState state) => $"bt-node-state-{state.ToString().ToLower()}";

        public static T CreateNodeAsset<T>(this BehaviourTree tree) where T : Node
		{
            return CreateNodeAsset(tree, typeof(T)) as T;
		}

        public static void CreateRootNodeAssetIfNeeded(this BehaviourTree tree)
		{
            if (tree != null && tree.Root == null)
			{
                CreateNodeAsset(tree, typeof(RootNode));
            }
		}

        public static Node CreateNodeAsset(this BehaviourTree tree, System.Type type)
		{

            var undoLabel = "Behaviour Tree (Create Node)";
            Undo.RecordObject(tree, undoLabel);

            var node = tree.CreateNode(type);
            Undo.RegisterCreatedObjectUndo(node, undoLabel);

            if (!Application.isPlaying)
			{
                AssetDatabase.AddObjectToAsset(node, tree);
            }

            return node;
		}

        public static void DeleteNodeAsset(this BehaviourTree tree, Node node)
		{

            var undoLabel = "Behaviour Tree (Delete Node)";
            Undo.RecordObject(tree, undoLabel);

            tree.DeleteNode(node);

            Undo.DestroyObjectImmediate(node);

            if (!Application.isPlaying)
			{
                AssetDatabase.SaveAssets();
            }
		}
    }
}

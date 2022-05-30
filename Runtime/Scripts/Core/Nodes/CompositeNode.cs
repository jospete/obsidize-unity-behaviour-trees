using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Obsidize.BehaviourTrees
{

    public abstract class CompositeNode : Node
    {
        
        [SerializeField]
		[HideInInspector]
		private List<Node> _children = new List<Node>();

        public List<Node> Children => _children;

		private int _currentIndex;

		protected abstract NodeState ExitState { get; }
		protected abstract NodeState AllChildrenProcessedState { get; }

		public override string PrimaryUssClass => "bt-composite";
		public sealed override NodeChildCapacity ChildCapacity => NodeChildCapacity.Multi;

		protected override void OnStart()
		{
			_currentIndex = 0;
		}

		public override bool DetachChild(Node node)
		{
			return Children.Remove(node);
		}

		public override List<Node> GetChildren()
		{
			return Children;
		}

		protected virtual void OnValidate()
		{
			_children.RemoveAll(x => x == null);
		}

		public void SortChildrenByGraphPositionHorizontal()
		{
			_children.Sort(CompareGraphPositionsHorizontal);
		}

		private int CompareGraphPositionsHorizontal(Node left, Node right)
		{
			return left.GraphPosition.x < right.GraphPosition.x ? -1 : 1;
		}

		public override Node Clone()
		{
			var result = Instantiate(this);

			result._children = _children
				.Where(x => x != null)
				.Select(x => x.Clone())
				.ToList();

			return result;
		}

		public override bool AttachChild(Node child)
		{

			var canAdd = !Children.Contains(child);

			if (canAdd)
			{
				Children.Add(child);
			}

			return canAdd;
		}

		protected override NodeState OnUpdate()
		{

			if (_currentIndex >= Children.Count)
			{
				_currentIndex = 0;
			}

			var child = Children[_currentIndex];

			if (child == null)
			{
				return NodeState.Failure;
			}

			var updatedState = child.Update();

			if (updatedState == NodeState.Running || updatedState == ExitState)
			{
				return updatedState;
			}

			_currentIndex++;

			if (_currentIndex >= Children.Count)
			{
				return AllChildrenProcessedState;
			}

			return NodeState.Running;
		}
	}
}

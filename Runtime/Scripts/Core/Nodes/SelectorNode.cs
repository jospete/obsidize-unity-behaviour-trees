using UnityEngine;

namespace Obsidize.BehaviourTrees
{

	public class SelectorNode : CompositeNode
	{

		protected sealed override NodeState ExitState => NodeState.Success;
		protected sealed override NodeState AllChildrenProcessedState => NodeState.Failure;
	}
}

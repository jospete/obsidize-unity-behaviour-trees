using UnityEngine;

namespace Obsidize.BehaviourTrees
{

	public class SequenceNode : CompositeNode
	{

		protected sealed override NodeState ExitState => NodeState.Failure;
		protected sealed override NodeState AllChildrenProcessedState => NodeState.Success;
	}
}

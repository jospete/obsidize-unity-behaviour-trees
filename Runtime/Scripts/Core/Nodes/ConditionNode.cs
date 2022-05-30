namespace Obsidize.BehaviourTrees
{

    public abstract class ConditionNode : ActionNode
    {

        public abstract bool IsTrue();

		protected override NodeState OnUpdate()
		{
			return IsTrue() ? NodeState.Success : NodeState.Failure;
		}
	}
}

namespace Obsidize.BehaviourTrees
{

    public abstract class ActionNode : Node
    {

		public override string PrimaryUssClass => "bt-action";
		public sealed override NodeChildCapacity ChildCapacity => NodeChildCapacity.None;
	}
}

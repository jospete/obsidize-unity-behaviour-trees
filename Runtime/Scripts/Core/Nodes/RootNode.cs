using UnityEngine;

namespace Obsidize.BehaviourTrees
{

	public class RootNode : ProxyNode
	{

		public override string PrimaryUssClass => "bt-root";

		protected override NodeState OnUpdate()
		{
			return Child.Update();
		}
	}
}

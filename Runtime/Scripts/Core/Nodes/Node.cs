using System.Collections.Generic;
using UnityEngine;

namespace Obsidize.BehaviourTrees
{

    public abstract class Node : ScriptableObject
    {

        [Space]
        [TextArea]
        [SerializeField]
        private string _description;

        [SerializeField]
        [HideInInspector]
        private string _guid;

        [SerializeField]
        [HideInInspector]
        private Vector2 _graphPosition;

        private NodeState _state = NodeState.Running;
        private bool _started = false;

        public virtual string DisplayName => name;
        public string Description => _description;
        public virtual string PrimaryUssClass => "bt-node";
        public abstract NodeChildCapacity ChildCapacity { get; }

        public NodeState State => _state;
        public bool Started => _started;
        public bool Idle => State == NodeState.Running && !Started;

        public string Guid
        {
            get => _guid;
            set => _guid = value;
        }

        public Vector2 GraphPosition
        {
            get => _graphPosition;
            set => _graphPosition = value;
        }

        protected abstract NodeState OnUpdate();

        protected virtual void OnStart()
        {
        }

        protected virtual void OnStop()
        {
        }

        public virtual bool AttachChild(Node child)
		{
            return false;
		}

        public virtual bool DetachChild(Node node)
		{
            return false;
		}

        public virtual List<Node> GetChildren()
		{
            return null;
		}

        public virtual Node Clone()
		{
            return Instantiate(this);
		}

        public NodeState Update()
		{

            if (!_started)
			{
                OnStart();
                _started = true;

            }

            _state = OnUpdate();

            if (_state == NodeState.Failure || _state == NodeState.Success)
			{
                OnStop();
                _started = false;
            }

            return _state;
        }

        protected virtual void OnTreeAwake(BehaviourTreeController tree)
        {

            var children = GetChildren();

            if (children == null)
            {
                return;
            }

            foreach (var child in children)
            {
                if (child != null)
                {
                    child.OnTreeAwake(tree);
                }
            }
        }
    }
}

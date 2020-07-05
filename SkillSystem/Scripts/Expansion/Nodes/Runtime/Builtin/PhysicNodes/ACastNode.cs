﻿using CabinIcarus.IcSkillSystem.Nodes.Runtime.Attributes;
using CabinIcarus.IcSkillSystem.Nodes.Runtime.Decorator;
using CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes;
using CabinIcarus.IcSkillSystem.SkillSystem.Runtime.Utils;
using NPBehave;
using UnityEngine;
using Node = XNode.Node;

namespace CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Nodes
{
    [Node.NodeWidthAttribute(200)]
    public abstract class ACastNode:AConditionNode
    {
        [Input(ShowBackingValue.Always,ConnectionType.Override,TypeConstraint.Strict)]
        [Label("Cast Owner")]
        [PortTooltip("no input use SkillGroup Owner")]
        private GameObject _owner;

        [PortTooltip("0 or less 0 is one Cast,less -1 Unlimited duration, else Every time Clock update Cast,Until the end of the duration")]
        [SerializeField,Input(ShowBackingValue.Always,ConnectionType.Override,TypeConstraint.Strict)]
        private IcVariableSingle _duration;
        
        [SerializeField,Input(ShowBackingValue.Always,ConnectionType.Override,TypeConstraint.Strict)]
        [Node.LabelAttribute("Layer Mask")]
        private IcVariableLayerMask _mask;
        
        protected LayerMask Mask => GetInputValue(nameof(_mask),_mask);

        public GameObject Owner => GetInputValue(nameof(_owner),SkillGroup.Owner);

        #region Debug

#if UNITY_EDITOR

        class DrawGizmosCom:MonoBehaviour
        {
            public System.Action OnDraw;
            public bool IsOpen;
            
            private void OnDrawGizmos()
            {
                if (IsOpen)
                {
                    OnDraw?.Invoke();
                }
            }

        }
        
        /// <summary>
        /// Only Editor
        /// </summary>
        public bool Debug;
        
        /// <summary>
        /// Only Editor
        /// </summary>
        public Color Color = Color.red;

        private DrawGizmosCom _drawGizmosCom;

        void _debugInit()
        {
            if (_drawGizmosCom == null)
            {
                GameObject go = new GameObject("Cast Node Debug");
                go.transform.SetParent(Owner.transform);
                _drawGizmosCom = go.AddComponent<DrawGizmosCom>();
                _drawGizmosCom.OnDraw += () => { Gizmos.color = Color; };
                _drawGizmosCom.OnDraw += OnDrawGizmos;
            }
        }
#endif
        protected void DebugStart()
        {
#if UNITY_EDITOR
            _debugInit();
            if (!Debug)
            {
                return;
            }
            _drawGizmosCom.IsOpen = true;
#endif
        }
        
        protected void DebugStop()
        {
#if UNITY_EDITOR
            _debugInit();
            if (!Debug)
            {
                return;
            }
            _drawGizmosCom.IsOpen = false;
#endif
        }

        protected virtual void OnDrawGizmos(){}
#endregion

        private float _time;
        
        protected sealed override bool Condition()
        {
            if (_time <= 0)
            {
                _time = GetInputValue(nameof(_duration), _duration);
            }
            
            bool result = OnCast();

            _time -= Time.deltaTime;

            if (_time <= -1)
            {
                return false;
            }

            if (_time <= 0)
            {
                return true;
            }

            return result;
        }

        protected abstract bool OnCast();

        [SerializeField]
        [PortTooltip("Max Ray cast Hit Result Count,default:100,min is 1")]
        [Min(1)]
        protected int MaxHitSize = 100;

        [SerializeField,Node.InputAttribute(Node.ShowBackingValue.Always,Node.ConnectionType.Override,Node.TypeConstraint.Strict)]
        [Node.LabelAttribute("Owner Add Offset")]
        private IcVariableVector3 _offset;

        protected Vector3 Origin => Owner.transform.position + Offset;
        
        protected Vector3 Offset => GetInputValue(nameof(_offset), _offset);
    }
}
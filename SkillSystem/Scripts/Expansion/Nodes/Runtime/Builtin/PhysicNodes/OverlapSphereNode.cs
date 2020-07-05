﻿using System.Collections.Generic;
using CabinIcarus.IcSkillSystem.Runtime.xNode_Nodes;
using UnityEngine;

namespace CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Nodes
{
    [CreateNodeMenu("CabinIcarus/IcSkillSystem/Behave Nodes/Condition/Physic/Overlap Sphere Cast")]
    public class OverlapSphereNode:ACast3DColliderNode
    {
        [SerializeField,Input(ShowBackingValue.Always,ConnectionType.Override,TypeConstraint.Inherited)]
        private IcVariableSingle _radius;

        private List<Collider> _result = new List<Collider>();
        
        protected override bool OnCast()
        {
            DebugStart();
            
            bool result = false;
            
            _result.Clear();
            
            var size = Physics.OverlapSphereNonAlloc(Origin, GetInputValue(nameof(_radius),_radius), Buffer, Mask,TriggerInteraction);

            if (size > 0)
            {
                for (var i = 0; i < size; i++)
                {
                    _result.Add(Buffer[i]);
                }

                result = true;
            }

            Result = _result;
            
            DebugStop();
            
            return result;
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Origin,GetInputValue(nameof(_radius),_radius));
        }
    }
}
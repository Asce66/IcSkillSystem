﻿using NPBehave;
using UnityEngine;

namespace CabinIcarus.IcSkillSystem.Nodes.Runtime.Tasks
{
    [CreateNodeMenu("CabinIcarus/IcSkillSystem/Behave Nodes/Task/Wait/Until Stopped")]
    [NodeWidth(300)]
    public class WaitUntilStoppedNode:ANPBehaveNode<WaitUntilStopped>
    {
        [SerializeField] 
        private bool _sucessWhenStopped;

        protected override WaitUntilStopped CreateOutValue()
        {
            return new WaitUntilStopped(_sucessWhenStopped);
        }
    }
}
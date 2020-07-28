//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年11月17日-22:06
//CabinIcarus.IcSkillSystem.Expansion.Runtime

using CabinIcarus.IcSkillSystem.Nodes.Runtime;
using NPBehave;
using UnityEngine;
using Action = NPBehave.Action;

namespace CabinIcarus.IcSkillSystem.Expansion.Runtime.Builtin.Nodes
{
    public abstract class AAnimatorNode:ANPBehaveNode<Task>
    {
        protected Animator Anim { get; private set; }

        protected override Task CreateOutValue()
        {
            Anim = SkillGraph.Owner.GetComponent<Animator>();
            
            return CreateAction();
        }

        protected abstract Task CreateAction();
    }
}
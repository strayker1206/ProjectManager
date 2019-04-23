using Neolant.SPF.Model.Data.Progress;
using Neolant.SPF.Model.Data.Progress.Realization;
using System;

namespace Neolant.SPF.NewUI.Services.NodeTypes.ProcessNodeTypes
{
    class NodeIStepped : NodeAbstractProcess
    {
        public new ISteppedProcess Process { get; }
        public INodeAbstractProcess Parent { get; }
        public override string Name => (Process as IProcess)?.Name;

        public NodeIStepped(ISteppedProcess process, INodeAbstractProcess parent) : base(process)
        {
            Process = process;
            Parent = parent;
        }
    }
}

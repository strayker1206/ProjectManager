using Neolant.SPF.Model.Data.Progress;
using Neolant.SPF.Model.Data.Progress.Realization;

namespace Neolant.SPF.NewUI.Services.NodeTypes.ProcessNodeTypes
{
    public class NodeIProcess : NodeAbstractProcess
    {
        public new IProcess Process { get; }
        public INodeAbstractProcess Parent { get; }
        public override string Name => Process.Name;

        public NodeIProcess(IProcess process, INodeAbstractProcess parent) : base(process)
        {
            Process = process;
            Parent = parent;
        }
    }
}

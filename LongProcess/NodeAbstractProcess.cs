using Neolant.SPF.Model.Core.Data;
using Neolant.SPF.Model.Data.Progress;
using Neolant.SPF.Model.Data.Progress.Realization;

namespace Neolant.SPF.NewUI.Services.NodeTypes.ProcessNodeTypes
{
    public abstract class NodeAbstractProcess : Notifier, INodeAbstractProcess
    {
        public IProcess Process { get; }
        public abstract string Name { get; }

        public NodeAbstractProcess(IProcess process)
        {
            Process = process;
        }
    }
}

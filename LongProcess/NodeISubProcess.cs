using Neolant.SPF.Model.Data.Progress;
using Neolant.SPF.Model.Data.Progress.Realization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Neolant.SPF.NewUI.Services.NodeTypes.ProcessNodeTypes
{
    public class NodeISubProcess : NodeAbstractProcess
    {
        public new ISubProcessed Process { get; }
        public override string Name => (Process as IProcess)?.Name;
        public ObservableCollection<INodeAbstractProcess> Processes { get; }

        public NodeISubProcess(ISubProcessed process) : base(process)
        {
            Process = process;
            Processes = new ObservableCollection<INodeAbstractProcess>();
        }
    }
}

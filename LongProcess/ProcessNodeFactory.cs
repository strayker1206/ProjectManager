using Neolant.SPF.Model.Data.Progress;
using Neolant.SPF.Model.Data.Progress.Realization;
using Neolant.SPF.NewUI.Resources.Localization.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neolant.SPF.NewUI.Services.NodeTypes.ProcessNodeTypes
{
    public class ProcessNodeFactory
    {
        public INodeAbstractProcess CreateProcessNode(IProcess process, INodeAbstractProcess parentNodeProcess = null)
        {
            if (process is ISubProcessed subProcess)
            {
                NodeISubProcess processNode = new NodeISubProcess(subProcess);

                foreach (IProcess proc in subProcess.SubProcesses)
                {
                    processNode.Processes.Add(CreateProcessNode(proc, processNode));
                }

                return processNode;
            }
            if (process is IStepped steppedProcess)
            {
                return new NodeIStepped(steppedProcess, parentNodeProcess);
            }
            if (process is IProcess)
            {
                return new NodeIProcess(process, parentNodeProcess);
            }

            throw new Exception(ServiceMessages.UnknownProcessType);
        }
    }
}

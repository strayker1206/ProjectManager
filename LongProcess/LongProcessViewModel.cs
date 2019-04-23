using Neolant.SPF.Model.Data.Progress;
using Neolant.SPF.NewUI.Resources.Localization.Messages;
using Neolant.SPF.NewUI.Services;
using Neolant.SPF.NewUI.Services.NodeTypes.ProcessNodeTypes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Neolant.SPF.NewUI.ViewModels._ProcessesAbstract
{
    public class LongProcessViewModel : BaseViewModel
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IProcess Process { get; private set; }
        public ObservableCollection<INodeAbstractProcess> Processes { get; private set; }

        public bool InProgress => Process.InProgress;
        public bool ProcessContainsError => Process.ProcessError != null;

        public LongProcessViewModel(IProcess process)
        {
            Process = process;
            CreateLoadingProcesses();
        }

        private void CreateLoadingProcesses()
        {
            Processes = new ObservableCollection<INodeAbstractProcess>();
            ProcessNodeFactory factory = new ProcessNodeFactory();
            try
            {
                Processes.Add(factory.CreateProcessNode(Process));
            }
            catch (Exception e)
            {
                logger.Error(e, DocumentMessages.ProcessTreeFormation);
                ShowErrorMessageBox(e.Message, DocumentMessages.ProcessTreeFormation);
            }
        }

        public virtual Task Run()
        {
            return Task.Run(() =>
            {
                (Process as IOperation).Run();
            });
        }
    }
}

using Neolant.SPF.Model.Data.Progress.Realization;

namespace Neolant.SPF.NewUI.Services.NodeTypes.ProcessNodeTypes
{
    public interface INodeAbstractProcess
    {
        IProcess Process { get; }
        string Name { get; }
    }
}

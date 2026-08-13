using System.Collections.ObjectModel;
using Diagrammer.Domain;

namespace Diagrammer.UI.Avalonia.ViewModels;

public class MainViewModel
{
    public ObservableCollection<DiagramNode> Nodes { get; } = new();

    public MainViewModel()
    {
        Nodes.Add(new GenericNode { Name = "Start Process", X = 50, Y = 50 });
        Nodes.Add(new GenericNode { Name = "Process Data", X = 250, Y = 100 });
        Nodes.Add(new GenericNode { Name = "End Process", X = 150, Y = 250 });
    }
}

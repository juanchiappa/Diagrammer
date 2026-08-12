using System.Threading.Tasks;
using Diagrammer.Domain;

namespace Diagrammer.Engine;

public interface IStorageService
{
    Task SaveGraphAsync(DiagramGraph graph, string filePath);
    Task<DiagramGraph> LoadGraphAsync(string filePath);
}

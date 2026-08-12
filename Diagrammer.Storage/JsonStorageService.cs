using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Diagrammer.Domain;
using Diagrammer.Engine;

namespace Diagrammer.Storage;

public class JsonStorageService : IStorageService
{
    private readonly JsonSerializerOptions _options;

    public JsonStorageService()
    {
        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task SaveGraphAsync(DiagramGraph graph, string filePath)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        using FileStream createStream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(createStream, graph, _options);
    }

    public async Task<DiagramGraph> LoadGraphAsync(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file at {filePath} was not found.");
        }

        using FileStream openStream = File.OpenRead(filePath);
        var graph = await JsonSerializer.DeserializeAsync<DiagramGraph>(openStream, _options);

        return graph ?? throw new InvalidOperationException("Failed to deserialize the diagram graph.");
    }
}

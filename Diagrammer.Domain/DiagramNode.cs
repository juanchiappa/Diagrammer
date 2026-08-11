using System;
using System.Collections.Generic;

namespace Diagrammer.Domain;

public abstract class DiagramNode
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public Dictionary<string, string> Metadata {get; set;} = new();
}
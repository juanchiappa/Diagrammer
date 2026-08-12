using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Diagrammer.Domain;

[JsonPolymorphic]
[JsonDerivedType(typeof(GenericNode), typeDiscriminator: "generic")]
public abstract class DiagramNode
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public Dictionary<string, string> Metadata {get; set;} = new();
}
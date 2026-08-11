using System;
using System.Collections.Generic;
using System.Linq;

namespace Diagrammer.Domain;

public class DiagramGraph
{
    public string ProjectName { get; set; } = "Nuevo Proyecto";
    public List<DiagramNode> Nodes { get; set; } = new();
    public List<DiagramEdge> Edges { get; set; } = new();

    public IEnumerable<DiagramEdge> GetEdgesForNode(Guid nodeId)
    {
        return Edges.Where(e => e.SourceNodeId == nodeId || e.TargetNodeId == nodeId);
    }
}
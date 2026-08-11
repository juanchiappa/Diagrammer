using System;
using System.Collections.Generic;

namespace Diagrammer.Domain;

public enum RelationshipType
{
    InheritsFrom,
    Implements,
    Composes,
    DependsOn,
    Invokes
}

public struct Point2D
{
    public double X { get; set; }
    public double Y { get; set; }
}

public class DiagramEdge
{
    public Guid Id {get; init;} = Guid.NewGuid();
    //math connects
    public Guid SourceNodeId {get; set;}
    public Guid TargetNodeId {get; set;}
    public RelationshipType Type {get; set;}
    //points
    public List<Point2D> Waypoints {get; set;}=new();    
}

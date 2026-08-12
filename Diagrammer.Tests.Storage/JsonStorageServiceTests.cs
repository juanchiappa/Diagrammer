using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Diagrammer.Domain;
using Diagrammer.Storage;
using Xunit;

namespace Diagrammer.Tests.Storage;

public class JsonStorageServiceTests
{
    [Fact]
    public async Task SaveAndLoadGraph_WithThreeNodesAndTwoEdges_MaintainsDataIntegrity()
    {
        // Arrange
        var storageService = new JsonStorageService();
        var tempFile = Path.GetTempFileName();
        
        try
        {
            var node1 = new GenericNode { Name = "Node 1", X = 10, Y = 10, Metadata = new Dictionary<string, string> { { "Key", "Value1" } } };
            var node2 = new GenericNode { Name = "Node 2", X = 20, Y = 20 };
            var node3 = new GenericNode { Name = "Node 3", X = 30, Y = 30 };

            var edge1 = new DiagramEdge
            {
                SourceNodeId = node1.Id,
                TargetNodeId = node2.Id,
                Type = RelationshipType.DependsOn,
                Waypoints = new List<Point2D> { new Point2D { X = 15, Y = 15 } }
            };

            var edge2 = new DiagramEdge
            {
                SourceNodeId = node2.Id,
                TargetNodeId = node3.Id,
                Type = RelationshipType.InheritsFrom
            };

            var originalGraph = new DiagramGraph
            {
                ProjectName = "Test Graph",
                Nodes = new List<DiagramNode> { node1, node2, node3 },
                Edges = new List<DiagramEdge> { edge1, edge2 }
            };

            // Act
            await storageService.SaveGraphAsync(originalGraph, tempFile);
            var loadedGraph = await storageService.LoadGraphAsync(tempFile);

            // Assert
            Assert.NotNull(loadedGraph);
            Assert.Equal(originalGraph.ProjectName, loadedGraph.ProjectName);
            
            // Nodes Assertions
            Assert.Equal(3, loadedGraph.Nodes.Count);
            
            var loadedNode1 = loadedGraph.Nodes.FirstOrDefault(n => n.Id == node1.Id);
            Assert.NotNull(loadedNode1);
            Assert.IsType<GenericNode>(loadedNode1);
            Assert.Equal(node1.Name, loadedNode1.Name);
            Assert.Equal(node1.X, loadedNode1.X);
            Assert.Equal(node1.Metadata["Key"], loadedNode1.Metadata["Key"]);

            // Edges Assertions
            Assert.Equal(2, loadedGraph.Edges.Count);
            
            var loadedEdge1 = loadedGraph.Edges.FirstOrDefault(e => e.Id == edge1.Id);
            Assert.NotNull(loadedEdge1);
            Assert.Equal(edge1.SourceNodeId, loadedEdge1.SourceNodeId);
            Assert.Equal(edge1.TargetNodeId, loadedEdge1.TargetNodeId);
            Assert.Equal(edge1.Type, loadedEdge1.Type);
            Assert.Single(loadedEdge1.Waypoints);
            Assert.Equal(edge1.Waypoints[0].X, loadedEdge1.Waypoints[0].X);
            Assert.Equal(edge1.Waypoints[0].Y, loadedEdge1.Waypoints[0].Y);
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}

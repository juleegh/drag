using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TestGameplay
{
    public class ShortestPath
    {
        private Dictionary<Vector2Int, Node> gridNodes;

        public void Initialize(List<GridCell> cells)
        {
            gridNodes = new Dictionary<Vector2Int, Node>();
            foreach (GridCell cell in cells)
            {
                gridNodes.Add(cell.Position, new Node(cell.Position));
            }

            foreach (Node node in gridNodes.Values.ToList())
            {
                if (gridNodes.ContainsKey(node.GetPosition() + Vector2Int.up))
                    node.AddNeighbourNode(gridNodes[node.GetPosition() + Vector2Int.up]);

                if (gridNodes.ContainsKey(node.GetPosition() + Vector2Int.down))
                    node.AddNeighbourNode(gridNodes[node.GetPosition() + Vector2Int.down]);

                if (gridNodes.ContainsKey(node.GetPosition() + Vector2Int.left))
                    node.AddNeighbourNode(gridNodes[node.GetPosition() + Vector2Int.left]);

                if (gridNodes.ContainsKey(node.GetPosition() + Vector2Int.right))
                    node.AddNeighbourNode(gridNodes[node.GetPosition() + Vector2Int.right]);
            }
        }

        public List<Vector2Int> findShortestPath(Vector2Int start, Vector2Int end)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            Node node = DijkstrasAlgo(start, end);

            while (node != null)
            {
                result.Add(node.GetPosition());
                node = node.GetParentNode();
            }

            result.Reverse();
            return result;
        }

        private Node DijkstrasAlgo(Vector2Int start, Vector2Int end)
        {
            List<Node> unexplored = new List<Node>();

            foreach (Node node in gridNodes.Values.ToList())
            {
                node.ResetNode();
                unexplored.Add(node);
            }

            Node startNode = gridNodes[start];
            startNode.SetWeight(0);

            while (unexplored.Count > 0)
            {
                unexplored.Sort((x, y) => x.GetWeight().CompareTo(y.GetWeight()));

                Node currentNode = unexplored[0];
                unexplored.Remove(currentNode);

                List<Node> neighbours = currentNode.GetNeighbourNodes();
                foreach (Node neighNode in neighbours)
                {
                    if (unexplored.Contains(neighNode))
                    {
                        float distance = Vector2Int.Distance(neighNode.GetPosition(), currentNode.GetPosition());
                        distance = currentNode.GetWeight() + distance;

                        if (distance < neighNode.GetWeight())
                        {
                            neighNode.SetWeight(distance);
                            neighNode.SetParentNode(currentNode);
                        }
                    }
                }

            }

            return gridNodes[end];
        }

    }

    public class Node
    {
        private float weight = int.MaxValue;
        private Node parentNode = null;
        private List<Node> neighbourNodes;
        private Vector2Int position;

        public Node(Vector2Int cellPosition)
        {
            position = cellPosition;
            neighbourNodes = new List<Node>();
            ResetNode();
        }

        public void ResetNode()
        {
            weight = int.MaxValue;
            parentNode = null;
        }

        public void SetParentNode(Node node) { parentNode = node; }
        public void SetWeight(float value) { weight = value; }
        public void AddNeighbourNode(Node node) { neighbourNodes.Add(node); }
        public List<Node> GetNeighbourNodes() { return neighbourNodes; }
        public float GetWeight() { return weight; }
        public Node GetParentNode() { return parentNode; }
        public Vector2Int GetPosition() { return position; }
    }
}
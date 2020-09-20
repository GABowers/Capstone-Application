using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class StaticMethods
    {
        public static List<BlankGrid> FindPath(AgentController agent, Tuple<int, int> destination, bool destinationEmpty) // how to handle destination containing something vs. being empty, or rather what to do when going to an object.
        {
            var startNode = new PathfindingNode(new Tuple<int, int>(agent.Cell.X, agent.Cell.Y), true);
            // if contains agent, then make "target node" check for something around it.
            HashSet<PathfindingNode> closedSet = new HashSet<PathfindingNode>();
            Heap<PathfindingNode> openSet = new Heap<PathfindingNode>(agent.Parent.gridHeight * agent.Parent.gridWidth);
            openSet.Add(startNode);
            PathfindingNode targetNode = new PathfindingNode(destination, destinationEmpty);
            while(openSet.Count > 0)
            {
                PathfindingNode currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);
                if(destinationEmpty)
                {
                    if (currentNode == targetNode)
                    {
                        return RetracePath(startNode, currentNode).Select(x => agent.Parent.grid[x.Position.Item1, x.Position.Item2]).ToList();
                    }
                }
                else
                {
                    List<PathfindingNode> targetNeighbors = GetMoveNeighborLocations(agent.Parent.GetStateInfo(agent.currentState), destination, new Tuple<int, int>(agent.Parent.gridWidth, agent.Parent.gridHeight)).Select(x => new PathfindingNode(x, !agent.Parent.grid[x.Item1, x.Item2].ContainsAgent)).ToList();
                    foreach (var targetNeighbor in targetNeighbors)
                    {
                        if(currentNode == targetNeighbor)
                        {
                            return RetracePath(startNode, currentNode).Select(x => agent.Parent.grid[x.Position.Item1, x.Position.Item2]).ToList();
                        }
                    }
                }

                List<PathfindingNode> neighbors = GetMoveNeighborLocations(agent.Parent.GetStateInfo(agent.currentState), new Tuple<int, int>(agent.X, agent.Y), new Tuple<int, int>(agent.Parent.gridWidth, agent.Parent.gridHeight)).Select(x => new PathfindingNode(x, !agent.Parent.grid[x.Item1, x.Item2].ContainsAgent)).ToList();

                foreach (var neighbor in neighbors)
                {
                    if(!neighbor.Empty || closedSet.Contains(neighbor))
                    {
                        continue;
                    }
                    int newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                    if(newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.GCost = newMovementCostToNeighbor;
                        neighbor.HCost = GetDistance(neighbor, targetNode);
                        neighbor.Parent = currentNode;
                        if(!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
            return null;
        }

        static List<PathfindingNode> RetracePath(PathfindingNode startNode, PathfindingNode endNode)
        {
            List<PathfindingNode> path = new List<PathfindingNode>();
            PathfindingNode currentNode = endNode;
            while(currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }

        static int GetDistance(PathfindingNode nodeA, PathfindingNode nodeB) //  type of distance?
        {
            int dstMin = Math.Min(Math.Abs(nodeA.Position.Item1 - nodeB.Position.Item1), Math.Abs(nodeA.Position.Item2 - nodeB.Position.Item2));
            int dstMax = Math.Max(Math.Abs(nodeA.Position.Item1 - nodeB.Position.Item1), Math.Abs(nodeA.Position.Item2 - nodeB.Position.Item2));
            return (14 * dstMin) + (10 * (dstMax - dstMin));
        }

        public static List<Tuple<int, int>> GetMoveNeighborLocations(CellState stateInfo, Tuple<int, int> position, Tuple<int, int> dimensions)
        {
            List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>();
            int mNeighborhood = (int)(stateInfo.mobileNeighborhood);
            if(mNeighborhood > 0)
            {
                neighbors.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2));
                neighbors.Add(new Tuple<int, int>(position.Item1 + 1, position.Item2));
                neighbors.Add(new Tuple<int, int>(position.Item1, position.Item2 - 1));
                neighbors.Add(new Tuple<int, int>(position.Item1, position.Item2 + 1));
                if(mNeighborhood > 1)
                {
                    neighbors.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2 - 1));
                    neighbors.Add(new Tuple<int, int>(position.Item1 + 1, position.Item2 + 1));
                    neighbors.Add(new Tuple<int, int>(position.Item1 + 1, position.Item2 - 1));
                    neighbors.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2 + 1));
                }
            }

            switch (stateInfo.gridType)
            {
                case GridType.Box:
                    neighbors = neighbors.Where(x => (x.Item1 >= 0) && (x.Item1 < dimensions.Item1) && (x.Item2 >= 0) && (x.Item2 < dimensions.Item2)).ToList();
                    break;
                case GridType.CylinderH:
                    neighbors = neighbors.Where(x => (x.Item1 >= 0) && (x.Item1 < dimensions.Item1)).ToList();
                    break;
                case GridType.CylinderW:
                    neighbors = neighbors.Where(x => (x.Item2 >= 0) && (x.Item2 < dimensions.Item2)).ToList();
                    break;
                case GridType.Torus:
                    break;
            }
            for (int i = 0; i < neighbors.Count; i++)
            {
                int x = neighbors[i].Item1;
                int y = neighbors[i].Item2;
                if(x < 0)
                {
                    x += dimensions.Item1;
                }
                else if(x >= dimensions.Item1)
                {
                    x -= dimensions.Item1;
                }
                if (y < 0)
                {
                    y += dimensions.Item2;
                }
                else if (y >= dimensions.Item2)
                {
                    y -= dimensions.Item2;
                }
                neighbors[i] = new Tuple<int, int>(x, y);
            }
            return neighbors;
        }

        public static bool PathOpen(List<BlankGrid> path)
        {
            return !(path.Any(x => x.ContainsAgent));
        }
    }

    class PathfindingNode : IHeapItem<PathfindingNode>
    {
        public Tuple<int, int> Position { get; private set; }
        public bool Empty { get; private set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost { get { return GCost + HCost; } }
        public PathfindingNode Parent { get; set; }
        public PathfindingNode(Tuple<int, int> pos, bool empty)
        {
            Position = pos;
            Empty = empty;
        }

        public int HeapIndex
        {
            get
            {
                return HeapIndex;
            }
            set
            {
                HeapIndex = value;
            }
        }

        public int CompareTo(PathfindingNode obj)
        {
            int compare = FCost.CompareTo(obj.FCost);
            if(compare == 0)
            {
                compare = HCost.CompareTo(obj.HCost);
            }
            return -compare;
        }
    }

    class Heap<T> where T : IHeapItem<T>
    {
        public T[] Items { get; private set; }
        public int CurrentItemCount { get; private set; }
        public Heap(int maxheapSize)
        {
            Items = new T[maxheapSize];
        }

        public void Add(T item)
        {
            item.HeapIndex = CurrentItemCount;
            Items[CurrentItemCount] = item;
            SortUp(item);
            CurrentItemCount++;
        }

        public T RemoveFirst()
        {
            T firstItem = Items[0];
            CurrentItemCount--;
            Items[0] = Items[CurrentItemCount];
            Items[0].HeapIndex = 0;
            SortDown(Items[0]);
            return firstItem;
        }

        public bool Contains(T item)
        {
            return Equals(Items[item.HeapIndex], item);
        }

        public int Count { get { return CurrentItemCount; } }

        public void UpdateItem(T item)
        {
            SortUp(item);
        }

        void SortDown(T item)
        {
            while(true)
            {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;
                int swapIndex = 0;
                if(childIndexLeft < CurrentItemCount)
                {
                    swapIndex = childIndexLeft;
                    if(childIndexRight < CurrentItemCount)
                    {
                        if(Items[childIndexLeft].CompareTo(Items[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }
                    if(item.CompareTo(Items[swapIndex]) < 0)
                    {
                        Swap(item, Items[swapIndex]);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        void SortUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;
            while(true)
            {
                T parentItem = Items[parentIndex];
                if(item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                {
                    break;
                }
            }
        }

        void Swap(T itemA, T itemB)
        {
            Items[itemA.HeapIndex] = itemB;
            Items[itemB.HeapIndex] = itemA;
            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }
    }

    public interface IHeapItem<T>:IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}

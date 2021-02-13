using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Application
{
    public class StaticMethods
    {
        public static List<BlankGrid> FindPath(AgentController agent, BlankGrid destination, bool destinationEmpty) // how to handle destination containing something vs. being empty, or rather what to do when going to an object.
        {
            var startNode = agent.Cell;
            // if contains agent, then make "target node" check for something around it.
            HashSet<BlankGrid> closedSet = new HashSet<BlankGrid>();
            //Heap<PathfindingNode> openSet = new Heap<PathfindingNode>(agent.Parent.gridHeight * agent.Parent.gridWidth);
            List<BlankGrid> openSet = new List<BlankGrid>();
            openSet.Add(startNode);
            List<BlankGrid> targetNeighbors = GetMoveNeighbors(agent.Parent.GetStateInfo(agent.currentState), destination, new Tuple<int, int>(agent.Parent.gridWidth, agent.Parent.gridHeight));
            while (openSet.Count > 0)
            {
                //PathfindingNode currentNode = openSet.RemoveFirst();// for heap implementation
                BlankGrid currentNode = openSet[0];
                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                    {
                        currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode);
                // end list version
                closedSet.Add(currentNode);
                if(destinationEmpty)
                {
                    if (currentNode == destination)
                    {
                        return RetracePath(startNode, currentNode);
                    }
                }
                else
                {
                    foreach (var targetNeighbor in targetNeighbors)
                    {
                        if(currentNode == targetNeighbor)
                        {
                            return RetracePath(startNode, currentNode);
                        }
                    }
                }

                List<BlankGrid> neighbors = GetMoveNeighbors(agent.Parent.GetStateInfo(agent.currentState), currentNode, new Tuple<int, int>(agent.Parent.gridWidth, agent.Parent.gridHeight));

                foreach (var neighbor in neighbors)
                {
                    if(neighbor.ContainsAgent || closedSet.Contains(neighbor))
                    {
                        continue;
                    }
                    int newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                    if(newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.GCost = newMovementCostToNeighbor;
                        neighbor.HCost = GetDistance(neighbor, destination);
                        neighbor.PathParent = currentNode;
                        if(!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                        //else
                        //{
                        //    openSet[openSet.IndexOf(neighbor)]
                        //}
                    }
                }
            }
            return null;
        }

        static List<BlankGrid> RetracePath(BlankGrid startNode, BlankGrid endNode)
        {
            List<BlankGrid> path = new List<BlankGrid>();
            BlankGrid currentNode = endNode;
            while(currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.PathParent;
            }
            path.Reverse();
            return path;
        }

        public static int GetDistance(BlankGrid nodeA, BlankGrid nodeB) //  type of distance?
        {
            int dstMin = Math.Min(Math.Abs(nodeA.X - nodeB.X), Math.Abs(nodeA.Y - nodeB.Y));
            int dstMax = Math.Max(Math.Abs(nodeA.X - nodeB.X), Math.Abs(nodeA.Y - nodeB.Y));
            return (14 * dstMin) + (10 * (dstMax - dstMin));
        }

        public static List<BlankGrid> GetMoveNeighbors(CellState stateInfo, BlankGrid position, Tuple<int, int> dimensions)
        {
            List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>();
            int mNeighborhood = (int)(stateInfo.mobileNeighborhood);
            if(mNeighborhood > 0)
            {
                neighbors.Add(new Tuple<int, int>(position.X - 1, position.Y));
                neighbors.Add(new Tuple<int, int>(position.X + 1, position.Y));
                neighbors.Add(new Tuple<int, int>(position.X, position.Y - 1));
                neighbors.Add(new Tuple<int, int>(position.X, position.Y + 1));
                if(mNeighborhood > 1)
                {
                    neighbors.Add(new Tuple<int, int>(position.X - 1, position.Y - 1));
                    neighbors.Add(new Tuple<int, int>(position.X + 1, position.Y + 1));
                    neighbors.Add(new Tuple<int, int>(position.X + 1, position.Y - 1));
                    neighbors.Add(new Tuple<int, int>(position.X - 1, position.Y + 1));
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
            List<BlankGrid> neigh = neighbors.Select(x => position.Parent.grid[(x.Item1 * dimensions.Item1) + x.Item2]).Where(xx => xx != null).ToList();
            return neigh;
        }

        public static bool PathOpen(List<BlankGrid> path)
        {
            return !(path.Any(x => x.ContainsAgent));
        }
    }

    //public class PathfindingNode
    //{
    //    public bool Walkable { get; private set; }
    //    public int GCost { get; set; }
    //    public int HCost { get; set; }
    //    public int FCost { get { return GCost + HCost; } }
    //    public PathfindingNode PathParent { get; set; }
    //    //private BlankGrid Parent;
    //    public Tuple<int, int> Position { get; private set; }

    //    public PathfindingNode(int x, int y, /*BlankGrid parent,*/ bool empty)
    //    {
    //        Walkable = empty;
    //        //this.Parent = parent;
    //        Position = new Tuple<int, int>(x, y);
    //    }

    //    //public BlankGrid GetParent()
    //    //{
    //    //    return Parent;
    //    //}

    //    public int HeapIndex { get; set; }
    //    public int CompareTo(PathfindingNode obj)
    //    {
    //        int compare = FCost.CompareTo(obj.FCost);
    //        if (compare == 0)
    //        {
    //            compare = HCost.CompareTo(obj.HCost);
    //        }
    //        return -compare;
    //    }
    //}

    class Heap<T> where T : IHeapItem<T>
    {
        T[] items;
        int currentItemCount;
        public Heap(int maxheapSize)
        {
            items = new T[maxheapSize];
        }

        public void Add(T item)
        {
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item);
            currentItemCount++;
        }

        public T RemoveFirst()
        {
            T firstItem = items[0];
            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);
            return firstItem;
        }

        public bool Contains(T item)
        {
            if(items.Contains(item))
            {
            }
            bool fake = Equals(items[item.HeapIndex], item); ;
            return fake;
        }

        public int Count { get { return currentItemCount; } }

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
                if(childIndexLeft < currentItemCount)
                {
                    swapIndex = childIndexLeft;
                    if(childIndexRight < currentItemCount)
                    {
                        if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }
                    if(item.CompareTo(items[swapIndex]) < 0)
                    {
                        Swap(item, items[swapIndex]);
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
                T parentItem = items[parentIndex];
                if(item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                {
                    break;
                }
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }

        void Swap(T itemA, T itemB)
        {
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;
            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }

        public T[] ToArray()
        {
            return items;
        }
    }

    public interface IHeapItem<T>:IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}

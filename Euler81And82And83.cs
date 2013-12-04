using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler81
{
    public enum Direction { Right, Down, Up, Left };

    class GridGraph
    {
        
        int[,] values;
        bool [] directions;

        public static GridGraph ReadFromText(string file,params Direction [] directions)
        {
            var gg = new GridGraph();

            gg.directions = new bool[4];
            foreach (var dir in directions)
            {
                gg.directions[(int)dir] = true;
            }

            var lines = File.ReadAllLines(file);
            int m = lines.Count();
            int n = lines.First().Split(',').Count();
            gg.values = new int[m, n];

            int i = 0;
            foreach (var line in lines)
            {
                int j = 0;
                foreach (var value in line.Split(','))
                {
                    gg.values[i, j] = int.Parse(value);
                    j++;
                }
                i++;
            }
            return gg;
        }

        private IEnumerable<Tuple<int, int>> GetNeighbors(Tuple<int, int> node,int m, int n)
        {
            if (node.Item1 != m - 1 && directions[(int)Direction.Down])
                yield return Tuple.Create(node.Item1 + 1, node.Item2);
            if (node.Item2 != n - 1 && directions[(int)Direction.Right])
                yield return Tuple.Create(node.Item1, node.Item2 + 1);
            if (node.Item1 != 0 && directions[(int)Direction.Up])
                yield return Tuple.Create(node.Item1-1, node.Item2);
            if (node.Item2 != 0 && directions[(int)Direction.Left])
                yield return Tuple.Create(node.Item1, node.Item2 - 1);
        }

        public int ShortestPath(Tuple<int, int> start = null)
        {
            var nodeToCost = TravelCosts(start);
            return nodeToCost[Tuple.Create(values.GetLength(0) - 1, values.GetLength(1) - 1)];
        }

        public int ShortestPathLeftColumn()
        {
            var travelCosts = from row in Enumerable.Range(0, values.GetLength(0)).AsParallel()
                              let nodeToCost = TravelCosts(Tuple.Create(row, 0))
                              select nodeToCost.Where(kvp => kvp.Key.Item2 == values.GetLength(1) - 1).Min(kvp => kvp.Value);
            return travelCosts.Min();
        }

        private Dictionary<Tuple<int,int>,int> TravelCosts(Tuple<int,int> start = null)
        {
            if (start == null)
            {
                start = Tuple.Create(0, 0);
            }

            int m = values.GetLength(0);
            int n = values.GetLength(1);
            
            
            var nonOriginNodes = from i in Enumerable.Range(0,m)
                                 from j in Enumerable.Range(0,n)
                                 where !(i==start.Item1 && j==start.Item2)
                                 select Tuple.Create(i,j);

            var costToNewNodes = new SortedDictionary<int, HashSet<Tuple<int, int>>>();
            costToNewNodes[values[start.Item1,start.Item2]] = new HashSet<Tuple<int,int>>{start};
            costToNewNodes[int.MaxValue] = new HashSet<Tuple<int, int>>(nonOriginNodes);

            var nodeToCost = nonOriginNodes.ToDictionary( t => t, t=> int.MaxValue);
            nodeToCost[start] = values[start.Item1, start.Item2];
            
            var explored = new HashSet<Tuple<int, int>>();

            while (costToNewNodes.Count > 0)
            {
                var kvp = costToNewNodes.First();
                var node = kvp.Value.First();

                kvp.Value.Remove(node);
                if (kvp.Value.Count == 0)
                {
                    costToNewNodes.Remove(kvp.Key);
                }

                explored.Add(node);

                var nodeCost = nodeToCost[node];

                foreach (var neighbor in GetNeighbors(node,m,n).Where(neighb => !explored.Contains(neighb)))
                {
                    
                    var currentNeighborCost = nodeToCost[neighbor];
                    var proposedNeighborCost = nodeCost + values[neighbor.Item1, neighbor.Item2];
                    if (proposedNeighborCost < currentNeighborCost)
                    {
                        nodeToCost[neighbor] = proposedNeighborCost;
                        costToNewNodes[currentNeighborCost].Remove(neighbor);
                        if (costToNewNodes[currentNeighborCost].Count == 0)
                        {
                            costToNewNodes.Remove(currentNeighborCost);
                        }

                        if (!costToNewNodes.ContainsKey(proposedNeighborCost))
                        {
                            costToNewNodes[proposedNeighborCost] = new HashSet<Tuple<int, int>>();
                        }
                        costToNewNodes[proposedNeighborCost].Add(neighbor);
                    }
                }

            }

            return nodeToCost;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Stopwatch();
            s.Start();
            //Euler 80:
            //var gg = GridGraph.ReadFromText(@"C:\Users\gumpy\Desktop\Euler\matrix.txt", Direction.Right,Direction.Down);


            //Euler 81:
            //var gg = GridGraph.ReadFromText(@"C:\Users\gumpy\Desktop\Euler\matrix.txt", Direction.Right, Direction.Down, Direction.Up);
            //var ans = gg.ShortestPathLeftColumn();

            //Euler 82:
            var gg = GridGraph.ReadFromText(@"C:\Users\gumpy\Desktop\Euler\matrix.txt", Direction.Right, Direction.Down,Direction.Up,Direction.Left);
            var ans = gg.ShortestPath();
            s.Stop();

            Console.WriteLine("Answer is {0}. Elapsed time is {1} seconds.", ans,s.Elapsed.TotalSeconds);
            Console.ReadLine();
        }
    }
}

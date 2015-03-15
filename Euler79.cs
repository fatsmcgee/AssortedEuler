using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler79.cs
{
    internal class Program
    {
        private static readonly int[] keys =
            {
                319,
                680,
                180,
                690,
                129,
                620,
                762,
                689,
                762,
                318,
                368,
                710,
                720,
                710,
                629,
                168,
                160,
                689,
                716,
                731,
                736,
                729,
                316,
                729,
                729,
                710,
                769,
                290,
                719,
                680,
                318,
                389,
                162,
                289,
                162,
                718,
                729,
                319,
                790,
                680,
                890,
                362,
                319,
                760,
                316,
                729,
                380,
                319,
                728,
                716
            };

        class DigitGraph
        {
            private Dictionary<int, HashSet<int>> edges;
            public DigitGraph()
            {
                edges = new Dictionary<int, HashSet<int>>();
            }

            public void AddEdge(int i, int j)
            {
                if (!edges.ContainsKey(i))
                {
                    edges[i] = new HashSet<int>();
                }

                edges[i].Add(j);
            }

            private static bool HasIncoming(Dictionary<int,HashSet<int>> graph, int toNode)
            {
                return graph.Any(kvp => kvp.Value.Contains(toNode));
            } 

            public List<int> TopologicalSort()
            {
                //make a copy of the original graph to allow mutation
                var edges = this.edges.ToDictionary(kvp => kvp.Key, kvp => new HashSet<int>(kvp.Value));

                var noEdges = edges.Keys.Where(k => !HasIncoming(edges, k));

                Stack<int> noEdgeNodes = new Stack<int>(noEdges);

                List<int> nodes = new List<int>();

                while (noEdgeNodes.Count > 0)
                {
                    var node = noEdgeNodes.Pop();
                    nodes.Add(node);

                    if (edges.ContainsKey(node))
                    {
                        var edgeChildren = edges[node].ToList();
                        edges[node].Clear();
                        foreach (var nextNode in edgeChildren.Where(e => !HasIncoming(edges, e)))
                        {
                            noEdgeNodes.Push(nextNode);
                        }
                    }

                }
                return nodes;

            } 
        }

        private static void Main(string[] args)
        {
            var dg = new DigitGraph();
            foreach (var key in keys)
            {
                var c = key%10;
                var b = (key/10)%10;
                var a = (key/100);
                dg.AddEdge(a, b);
                dg.AddEdge(b, c);
            }

            var orderedNodes = dg.TopologicalSort();
            var answer = orderedNodes.AsEnumerable().Aggregate((acc, node) => acc*10 + node);

            Console.WriteLine("Answer is " + answer);
            Console.ReadLine();
        }
    }
}
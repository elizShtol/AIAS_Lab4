using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class TravelingSalesmansTask
    {
        public readonly int[,] Graph;

        public TravelingSalesmansTask(int[,] graph)
        {
            if (graph.GetLength(0) != graph.GetLength(1))
                throw new Exception("Graph matrix must be square");
            Graph = graph;
        }

        public (int[] path, int length) Solve()
        {
            var permutations = GetPermutations(Graph.GetLength(0));
            var bestPathLength = int.MaxValue;
            var pathWasFound = false;
            var bestPermutation = new int[permutations[0].Length + 1];
            foreach (var permutation in permutations)
            {
                var pathLength = Evaluate(permutation);
                if (pathLength != null && pathLength < bestPathLength)
                {
                    Array.Copy(permutation, bestPermutation, permutation.Length);
                    bestPathLength = (int) pathLength;
                    pathWasFound = true;
                    bestPermutation[permutation.Length] = bestPermutation[0];
                }
            }

            if (pathWasFound)
                return (bestPermutation, bestPathLength);
            throw new Exception("Path can't be found");
        }

        private int? Evaluate(int[] permutation)
        {
            var previous = permutation[0];
            var result = 0;
            for (int i = 1; i <= permutation.Length; i++)
            {
                var path = i < permutation.Length ? Graph[previous, permutation[i]] : Graph[previous, permutation[0]];
                if (path == 0)
                    return null;
                result += path;
                previous = i < permutation.Length ? permutation[i] : 0;
            }

            return result;
        }
        
        public static List<int[]> GetPermutations(int n)
        {
            if (n <= 0)
                throw new Exception("N must be positive");
            var permutations = new List<int[]>();
            MakePermutations(new int[n], 0, permutations);
            return permutations;
        }
        
        private static void MakePermutations(int[] permutation, int position, List<int[]> permutations)
        {
            if (position == permutation.Length)
            {
                var permutationCopy = new int[permutation.Length];
                Array.Copy(permutation, permutationCopy, permutation.Length);
                permutations.Add(permutationCopy);
                return;
            }

            for (int i = 0; i < permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, i, 0, position);
                if (index != -1)
                    continue;
                permutation[position] = i;
                MakePermutations(permutation, position + 1, permutations);
            }
        }
    }
    
    
}
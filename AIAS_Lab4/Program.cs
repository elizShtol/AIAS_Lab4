using System;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Введите количество вершин в графе");
                var nodesCount = int.Parse(Console.ReadLine());
                var distanceMatrix = new double[nodesCount, nodesCount];
                Console.WriteLine("Введите матрицу расстояний графа");
                try
                {
                    var graph = ReadGraph(nodesCount);
                    var result = new TravelingSalesmansTask(graph).Solve();
                    Console.WriteLine("Порядок обхода вершин графа: ");
                    foreach (var item in result.path)
                        Console.Write($"{item+1} ");
                    Console.WriteLine();
                    Console.WriteLine($"Длина пути: {result.length}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); 
                }
                Console.WriteLine();
                Console.WriteLine("Для завершения работы программы нажмите y");
                if (Console.ReadKey() == new ConsoleKeyInfo('y', ConsoleKey.Y, false, false, false))
                    break;
                Console.WriteLine();
            }
            
        }

        private static int[,] ReadGraph(int nodesCount)
        {
            var graph = new int[nodesCount, nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                while (true)
                {
                    var items = Console.ReadLine().Trim().Split();
                    if (items.Length == nodesCount)
                    {
                        for (int j = 0; j < nodesCount; j++)
                        {
                            graph[i, j] = int.Parse(items[j]);
                        }
                        break;
                    }
                        
                    Console.WriteLine($"Введите {nodesCount} значения");
                }
            }

            return graph;
        }
    }
}
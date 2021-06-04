using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ConsoleApp1;

namespace AIAS_Lab4_Tests
{
    public class TravellingSalesmansTaskTests
    {
        public static IEnumerable<TestCaseData> PermutationsTests()
        {
            var test = new TestCaseData(1, new []{new []{0}});
            test.TestName = "ElementsCount1";
            yield return test;
            test = new TestCaseData(3, new []{new []{0,1,2}, new []{0,2,1}, new []{1,0,2}, new []{1,2,0}, 
                new []{2,0,1}, new []{2,1,0}});
            test.TestName = "ElementsCountMoreThan1";
            yield return test;
        }
        
        public static IEnumerable<TestCaseData> SolveTests()
        {
            var test = new TestCaseData(new [,]{{0,1},{1,0}}, new []{0,1,0}, 2);
            test.TestName = "TwoNodes";
            yield return test;
            test = new TestCaseData(new [,]{{0, 4,5,7,5}, {8, 0, 5,6,6}, {3,5,0,9,6}, {3,5,6,0,2}, {6,2,3,8,0}}, new []{0,1,3,4,2,0}, 18);
            test.TestName = "ManyNodes";
            yield return test;
        }


        [Test]
        [TestCaseSource("PermutationsTests")]
        public void GetPermutations(int n, int[][] expected)
        {
            var actual = TravelingSalesmansTask.GetPermutations(n).ToArray();
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.That(Enumerable.SequenceEqual(expected[i], actual[i]));
            }
        }

        [Test]
        public void GetPermutationsShouldThrows()
        {
            Assert.Throws<Exception>(() => TravelingSalesmansTask.GetPermutations(0));
            Assert.Throws<Exception>(() => TravelingSalesmansTask.GetPermutations(-1));
        }

        
        [Test]
        [TestCaseSource("SolveTests")]
        public void Solve(int[,] graph, int[] expectedPath, int expectedLength)
        {
            var actual = new TravelingSalesmansTask(graph).Solve();
            Assert.That(Enumerable.SequenceEqual(expectedPath, actual.path));
            Assert.AreEqual(expectedLength, actual.length);
        }

        [Test]
        public void SolveShouldThrows()
        {
            var graph = new[,] {{0, 1}};
            Assert.Throws<Exception>(() => new TravelingSalesmansTask(graph).Solve());
            graph = new[,] {{0}};
            Assert.Throws<Exception>(() => new TravelingSalesmansTask(graph).Solve());
            graph = new[,] {{0, 1}, {0,0}};
            Assert.Throws<Exception>(() => new TravelingSalesmansTask(graph).Solve());
        }
    }
}
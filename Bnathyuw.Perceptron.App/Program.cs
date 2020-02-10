using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Math;

namespace Bnathyuw.Perceptron.App
{
    class Program
    {
        private const double TrainingRate = 0.1d;

        static void Main(string[] args)
        {
            var weights = TrainNetwork();

            TestNetwork(weights);
        }
        
        private static double[] TrainNetwork()
        {
            var trainingCases = File.ReadLines("Data/training_10_6_1.csv");
            var weights = new[] {0d, 0d, 0d};
            foreach (var (inputs, actualMatch) in trainingCases.Select(ParseValues))
            {
                Console.WriteLine($"input {string.Join(',', inputs)}");
                Console.WriteLine($"weights {string.Join(',', weights)}");
                var predictedMatch = Calculate(inputs, weights);
                Console.WriteLine($"output: {predictedMatch}, expected: {actualMatch}");

                double AdjustWeight(double input, double weight) =>
                    weight + TrainingRate * (actualMatch - predictedMatch) * input;

                weights = inputs.Zip(weights, AdjustWeight).ToArray();
            }

            return weights;
        }

        private static void TestNetwork(double[] weights)
        {
            var testCases = File.ReadLines("Data/test_10_6_1.csv");
            var results = testCases.Select(ParseValues)
                .Select(tuple => Calculate(tuple.input, weights) == tuple.region);

            Console.WriteLine($"Total correct {results.Count(x => x)}; total incorrect {results.Count(x => !x)}");
        }

        private static int Calculate(IEnumerable<double> inputs, IEnumerable<double> weights) =>
            Sign(inputs.Zip(weights, Product).Sum());

        private static double Product(double input, double weight) => input * weight;

        private static (double[] input, int region) ParseValues(string first)
        {
            var strings = first.Split(",");
            var x = double.Parse(strings[0]);
            var y = double.Parse(strings[1]);
            var match = strings[2] == "A" ? 1 : -1;
            return (new[] {1d, x, y}, match);
        }
    }
}
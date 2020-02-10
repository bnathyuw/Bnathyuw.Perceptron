using System;
using System.IO;
using System.Linq;

namespace Bnathyuw.Perceptron.App
{
    class Program
    {
        static void Main(string[] args)
        {
            const double trainingRate = 0.1d;
            var trainingCases = File.ReadLines("Data/training_10_6_1.csv");
            var weights = new[] {0d, 0d, 0d};
            foreach (var (input, region) in trainingCases.Select(ParseValues))
            {
                Console.WriteLine($"input {string.Join(',', input)}");
                Console.WriteLine($"weights {string.Join(',', weights)}");
                var output = Calculate(input, weights);
                Console.WriteLine($"output: {output}, expected: {region}");
                var newWeight0 = weights[0] + trainingRate * (region - output) * input[0];
                var newWeight1 = weights[1] + trainingRate * (region - output) * input[1];
                var newWeight2 = weights[2] + trainingRate * (region - output) * input[2];
                weights = new[] {newWeight0, newWeight1, newWeight2};
            }

            var testCases = File.ReadLines("Data/test_10_6_1.csv");
            var correct = 0;
            var incorrect = 0;
            foreach (var (input, region) in testCases.Select(ParseValues))
            {
                var output = Calculate(input, weights);
                if (output == region)
                {
                    Console.WriteLine("Correct!");
                    correct++;
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                    incorrect++;
                }
            }
            Console.WriteLine($"Total correct {correct}; total incorrect {incorrect}");
        }

        private static int Calculate(double[] input, double[] weights)
        {
            return Math.Sign(input[0] * weights[0] + input[1] * weights[1] + input[2] * weights[2]);
        }

        private static (double[] input, int region) ParseValues(string first)
        {
            var strings = first.Split(",");
            var x = double.Parse(strings[0]);
            var y = double.Parse(strings[1]);
            var region = strings[2] == "A" ? 1 : -1;
            return (new[]{1d, x, y}, region);

        }
        
        
    }

    public enum Region
    {
        A, B
    }
}
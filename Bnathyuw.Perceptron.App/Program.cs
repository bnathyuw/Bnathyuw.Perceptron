using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Bnathyuw.Perceptron.TestDataGenerator;
using static System.Math;

namespace Bnathyuw.Perceptron.App
{
    class Program
    {
        private const double TrainingRate = 0.1d;
        private const int NumberOfInputs = 2;
        private const string TestDataFile = "Data/test_10_6_1.csv";
        private const string TrainingDataFile = "Data/training_10_6_1.csv";
        private const double Radius = 10;
        private const double Width = 6;
        private const double Separation = 1;

        private static PointGenerator PointGenerator;
        static Program() => PointGenerator = new PointGenerator(Radius, Width, Separation);

        static void Main(string[] args)
        {
            TestFileData();

            TestGeneratedData();
        }

        private static void TestFileData()
        {
            var neuron = Train(Neuron.Untrained(NumberOfInputs, TrainingRate), ReadDataFrom(TrainingDataFile));

            var results = Test(neuron, ReadDataFrom(TestDataFile));
            
            Console.WriteLine("File Data");
            OutputResults(results);
        }

        private static void TestGeneratedData()
        {
            var neuron = Train(Neuron.Untrained(NumberOfInputs, TrainingRate), GenerateData(1000));

            var results = Test(neuron, GenerateData(2000));

            Console.WriteLine("Generated Data");
            OutputResults(results);
        }

        private static void OutputResults((double actual, double predicted)[] results)
        {
            Console.WriteLine($"Confusion Matrix:");
            Console.WriteLine($"            | Actual T | Actual F");
            Console.WriteLine(
                $"Predicted T | {results.Count(x => x.actual > 0 && x.predicted > 0),8} | {results.Count(x => x.actual < 0 && x.predicted > 0),8}");
            Console.WriteLine(
                $"Predicted F | {results.Count(x => x.actual > 0 && x.predicted < 0),8} | {results.Count(x => x.actual < 0 && x.predicted < 0),8}");
        }

        private static IEnumerable<DataRow> ReadDataFrom(string file) => File.ReadLines(file).Select(ParseValues);

        private static IEnumerable<DataRow> GenerateData(int numberOfDataPoints) =>
            PointGenerator.GeneratePoints(numberOfDataPoints)
                .Select(dp => new DataRow(new[] {dp.X, dp.Y, dp.Region == Region.A ? 1 : -1}));

        private static DataRow ParseValues(string line) => 
            new DataRow(ParseCsvAsDoubles(line));

        private static double[] ParseCsvAsDoubles(string line) => 
            line.Split(",").Select(double.Parse).ToArray();

        private static Neuron Train(Neuron seedNeuron, IEnumerable<DataRow> trainingData) =>
            trainingData.Aggregate(seedNeuron, (current, dataRow) => current.Calibrate(dataRow));

        private static (double actual, double predicted)[] Test(Neuron neuron, IEnumerable<DataRow> testData) =>
            testData.Select(dataRow => (actual: dataRow.Output, predicted: neuron.Calculate(dataRow)))
                .ToArray();
    }
}
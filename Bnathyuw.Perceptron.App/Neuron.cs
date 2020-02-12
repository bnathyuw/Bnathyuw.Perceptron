using System.Linq;
using static System.Linq.Enumerable;
using static System.Math;

namespace Bnathyuw.Perceptron.App
{
    internal class Neuron
    {
        private readonly double[] _weightVector;
        private readonly double _trainingRate;

        private Neuron(double[] weightVector, double trainingRate)
        {
            _weightVector = weightVector;
            _trainingRate = trainingRate;
        }

        public double Predict(DataRow inputVector) => 
            inputVector.InputVector
                .Zip(_weightVector, Product)
                .Sum()
                .Pipe(Sign);

        private static double Product(double x, double y) => x * y;

        public Neuron Calibrate(DataRow dataRow) =>
            dataRow.InputVector
                .Zip(_weightVector, (input, weight) => input * WeightedDifferenceFromActual(dataRow) + weight)
                .ToArray()
                .Pipe(x => new Neuron(x, _trainingRate));

        private double WeightedDifferenceFromActual(DataRow dataRow) => _trainingRate * DifferenceFromActual(dataRow);

        private double DifferenceFromActual(DataRow dataRow) => dataRow.Output - Predict(dataRow);

        public static Neuron Untrained(int numberOfInputs, double trainingRate) =>
            Repeat(0d, numberOfInputs + 1)
                .ToArray()
                .Pipe(x => new Neuron(x, trainingRate));
    }
}
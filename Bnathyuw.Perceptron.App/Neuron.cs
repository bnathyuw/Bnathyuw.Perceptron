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

        public double Calculate(DataRow inputVector) => 
            Sign(inputVector.InputVector.Zip(_weightVector, Product).Sum());

        private static double Product(double x, double y) => x * y;

        public Neuron Calibrate(DataRow dataRow)
        {
            var outputDifference = dataRow.Output - Calculate(dataRow);

            double AdjustWeight(double input, double weight) => 
                weight + _trainingRate * outputDifference * input;

            var newWeights = dataRow.InputVector.Zip(_weightVector, AdjustWeight).ToArray();
            
            return new Neuron(newWeights, _trainingRate);
        }

        public static Neuron Untrained(int numberOfInputs, double trainingRate) => 
            new Neuron(Repeat(0d, numberOfInputs + 1).ToArray(), trainingRate);
    }
}
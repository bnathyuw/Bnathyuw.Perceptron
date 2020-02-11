using System.Linq;

namespace Bnathyuw.Perceptron.App
{
    internal class DataRow
    {
        public DataRow(double[] values) => _values = values;

        private readonly double[] _values;

        public double Output => _values[^1];

        public double[] InputVector => new []{1d}.Concat(_values[..^1]).ToArray();
    }
}
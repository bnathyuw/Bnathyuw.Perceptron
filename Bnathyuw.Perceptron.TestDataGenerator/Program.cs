using System;
using System.Globalization;

namespace Bnathyuw.Perceptron.TestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class PointGenerator
    {
        private Random _random;

        public PointGenerator()
        {
            _random = new Random();
        }
        public Point GeneratePoint()
        {
            return new Point
            {
                X = _random.Next(),
                Y = _random.Next(),
                Region = (Region) _random.Next(0,1)
            };
        }
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Region Region { get; set; }
    }
    
    public enum Region
    {
        A, B
    }
}

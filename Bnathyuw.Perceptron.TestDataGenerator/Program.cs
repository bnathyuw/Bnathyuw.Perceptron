using System;
using System.Collections.Generic;
using static System.Math;

namespace Bnathyuw.Perceptron.TestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var pointGenerator = new PointGenerator(10, 6, 1);


            var points = pointGenerator.GeneratePoints(2000);
            foreach (var point in points)
            {
                Console.WriteLine(point);
            }
        }
    }

    public class PointGenerator
    {
        private readonly double _radius;
        private readonly double _width;
        private readonly double _separation;
        private readonly Random _random;

        public PointGenerator(double radius, double width, double separation)
        {
            _radius = radius;
            _width = width;
            _separation = separation;
            _random = new Random();
        }
        public Point GeneratePoint()
        {
            var region = (Region) _random.Next(2);
            var distance = _radius - _random.NextDouble() * _width;
            var (startAngle, centreX, centreY) = GetMetrics(region);
            var angle = startAngle + _random.NextDouble() * PI;
            var x = centreX + Cos(angle) * distance;
            var y = centreY + Sin(angle) * distance;
            return new Point
            {
                X = x,
                Y = y,
                Region = region
            };
        }

        private (double startAngle, double centreX, double centreY) GetMetrics(Region region)
        {
            switch (region)
            {
                case Region.A:
                {
                    return (0, 0, 0);
                }
                case Region.B:
                {
                    return (PI, _radius - _width / 2, -_separation);
                }
                default:
                    throw new NotImplementedException();
            }
        }

        public IEnumerable<Point> GeneratePoints(int numberOfPointsToCreate)
        {
            var points = new List<Point>();
            for (var i = 0; i < numberOfPointsToCreate; i++)
            {
                var point = GeneratePoint();
                points.Add(point);
            }

            return points;
        }
    }

    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Region Region { get; set; }

        public override string ToString()
        {
            return $"{X}, {Y}, {Region}";
        }
    }
    
    public enum Region
    {
        A, B
    }
}

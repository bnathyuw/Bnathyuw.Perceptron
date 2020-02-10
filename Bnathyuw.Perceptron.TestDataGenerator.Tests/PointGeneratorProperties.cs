using System;
using FluentAssertions;
using FsCheck.Xunit;
using static System.Math;

namespace Bnathyuw.Perceptron.TestDataGenerator.Tests
{
    public class PointGeneratorProperties
    {
        private const double Radius = 10;
        private const double Width = 6;
        private const double Separation = 1;
        private readonly PointGenerator _pointGenerator;

        public PointGeneratorProperties()
        {
            _pointGenerator = new PointGenerator(Radius, Width, Separation);
        }

        [Property]
        public void PointsShouldBeDifferent()
        {
            var point = _pointGenerator.GeneratePoint();
            var anotherPoint = _pointGenerator.GeneratePoint();
            point.Should().NotBe(anotherPoint);
        }

        [Property]
        public void PointsShouldLieWithinRadius()
        {
            var point = _pointGenerator.GeneratePoint();
            switch (point.Region)
            {
                case Region.A:
                {
                    var distanceFromCentre = Sqrt(point.X * point.X + point.Y * point.Y);
                    distanceFromCentre.Should().BeLessOrEqualTo(Radius);
                    break;
                }
                case Region.B:
                {
                    const double centreX = Radius - Width / 2d;
                    const double centreY = -Separation;
                    var distanceX = point.X - centreX;
                    var distanceY = point.Y - centreY;
                    var distanceFromCentre = Sqrt(distanceX * distanceX + distanceY * distanceY);
                    distanceFromCentre.Should().BeLessOrEqualTo(Radius);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Property]
        public void PointsInRegionAShouldLieWithinWidthOfMoon()
        {
            var point = _pointGenerator.GeneratePoint();
            switch (point.Region)
            {
                case Region.A:
                {
                    var distanceFromCentre = Sqrt(point.X * point.X + point.Y * point.Y);
                    var distanceFromCircumference = Radius - distanceFromCentre;
                    distanceFromCircumference.Should().BeLessOrEqualTo(Width);
                    break;
                }
                case Region.B:
                {
                    const double centreX = Radius - Width / 2d;
                    const double centreY = -Separation;
                    var distanceX = point.X - centreX;
                    var distanceY = point.Y - centreY;
                    var distanceFromCentre = Sqrt(distanceX * distanceX + distanceY * distanceY);
                    var distanceFromCircumference = Radius - distanceFromCentre;
                    distanceFromCircumference.Should().BeLessOrEqualTo(Width);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
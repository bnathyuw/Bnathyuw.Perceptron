using FluentAssertions;
using FsCheck.Xunit;

namespace Bnathyuw.Perceptron.TestDataGenerator.Tests
{
    public class PointGeneratorProperties
    {
        [Property]
        public void PointsShouldBeDifferent()
        {
            var pointGenerator = new PointGenerator();
            var point = pointGenerator.GeneratePoint();
            var anotherPoint = pointGenerator.GeneratePoint();
            point.Should().NotBe(anotherPoint);
        }
    }
}
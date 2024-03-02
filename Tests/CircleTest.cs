using Xunit;

namespace TestTaskMindbox
{

    public class CircleTest 
    {
        [Fact]
        public void TestCalculateArea()
        {
            var radius = 5.0;
            var circle = new Circle(radius);
            var visitor = new AreaCalculateVisitor();

            var result = circle.CalculateArea(visitor);

            Assert.Equal(Math.PI * radius * radius, result);
        }
    }
}

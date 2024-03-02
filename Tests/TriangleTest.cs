using Xunit;

namespace TestTaskMindbox
{
    public class TriangleTest
    {
        [Fact]
        public void TestCheckIsTriangleIsRight()
        {
            var triangle = new Triangle(15, 12, 9);
            var result = triangle.CheckIfTriangleIsRight();

            Assert.True(result);
        }

        [Fact]
        public void TestCalculateArea()
        {
            var sideA = 3.0;
            var sideB = 4.0;
            var sideC = 5.0;
            var triangle = new Triangle(sideA, sideB, sideC);
            var visitor = new AreaCalculateVisitor();

            var result = triangle.CalculateArea(visitor);

            var expectedResult = 0.5 * sideA * sideB;
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void TestIfFigureNotTriangle()
        {
            var sideA = 1.0;
            var sideB = 1.0;
            var sideC = 3.0;
            var triangle = new Triangle(sideA, sideB, sideC);
            var visitor = new AreaCalculateVisitor();

            var result = triangle.CalculateArea(visitor);

            Assert.Equal(0.0, result);
        }
    }
}

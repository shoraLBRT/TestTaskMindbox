namespace TestTaskMindbox
{
    public class AreaCalculateVisitor : IAreaVisitor
    {
        public double Visit(Circle circle)
        {
            return Math.PI * circle.radius * circle.radius;
        }

        public double Visit(Triangle triangle)
        {
            double perimeter = (triangle.sideA + triangle.sideB + triangle.sideC) / 2;
            double area = Math.Sqrt(perimeter * (perimeter - triangle.sideA) * (perimeter - triangle.sideB) * (perimeter - triangle.sideC));
            return area;
        }
    }
}

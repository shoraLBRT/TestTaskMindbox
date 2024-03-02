namespace TestTaskMindbox
{
    public class Circle : IFigure
    {
        public double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public double CalculateArea(IAreaVisitor areaCalculateVisitor)
        {
            return areaCalculateVisitor.Visit(this);
        }
    }
}

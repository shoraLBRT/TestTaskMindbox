namespace TestTaskMindbox
{
    public class Triangle : IFigure
    {
        public double sideA;
        public double sideB;
        public double sideC;
        public Triangle(double sideA, double sideB, double sideC)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
        }
        public bool CheckIfTriangleIsRight()
        {
            // Сортировка сторон по возрастанию
            var sides = new[] { sideA, sideB, sideC };
            Array.Sort(sides);

            // Проверка условия теоремы Пифагора
            return Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) == Math.Pow(sides[2], 2);
        }

        public double CalculateArea(IAreaVisitor areaCalculateVisitor)
        {
            if (!IsTriangle())
                return 0;
            return areaCalculateVisitor.Visit(this);
        }
        private bool IsTriangle()
        {
            if (!(sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA))
                return false;
            return true;
        }
    }
}

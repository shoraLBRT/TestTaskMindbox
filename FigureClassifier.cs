namespace TestTaskMindbox
{
    public class FigureClassifier
    {
        // при необходимости добавления фигур, мы добавляем одну проверку на валидность фигуре в строку, раасположенную
        // по убыванию количества необходимых параметров (для квадрата нужно 2 параметра, поэтому между треугольником и кругом)
        public IFigure? Classify(double sideA, double? sideB, double? sideC)
        {
            if (IsTriangle(sideA, sideB, sideC))
                return new Triangle(sideA, sideB!.Value, sideC!.Value);

            if (isCircle(sideA, sideB, sideC))
                return new Circle(sideA);

            return null;
        }
        private bool IsTriangle(double a, double? b, double? c)
        {
            return (a > 0 && b.HasValue && b.Value > 0 && c.HasValue && c.Value > 0);
        }
        private bool isCircle(double a, double? b, double? c)
        {
            if (b.HasValue || c.HasValue)
                return false;
            return (a > 0);
        }
    }
}

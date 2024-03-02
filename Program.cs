using TestTaskMindbox;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var visitor = new AreaCalculateVisitor();

// Здесь я не понял, какой реализации вы ожидаете, когда упоминаете 3 пункт: "Вычисление площади фигуры без знания типа фигуры в compile-time"
// В действительности, нижеприведенное решение удовлетворяет ваше требование
#region Первый подход

app.MapGet("/CalculateArea/Circle/{radius}", async (HttpContext context, double radius) =>
{
    var circle = new Circle(radius);
    double CircleArea = circle.CalculateArea(visitor);

    if (CircleArea > 0)
        await context.Response.WriteAsync($"The area of the circle is: {CircleArea}");
    else
        await context.Response.WriteAsync($"Input parameter of the circle is not valid");
});

app.MapGet("/CalculateArea/Triangle/{sideA}/{sideB}/{sideC}", async (HttpContext context, double sideA, double sideB, double sideC) =>
{
    var triangle = new Triangle(sideA, sideB, sideC);
    double TriangleArea = triangle.CalculateArea(visitor);

    if (TriangleArea > 0)
    {
        bool isRightTriangle = triangle.CheckIfTriangleIsRight();

        await context.Response.WriteAsync($"The area of the triangle is: {TriangleArea} \n" +
            $"Is the Triangle right-angled? - {isRightTriangle}");
    }
    else
        await context.Response.WriteAsync($"Input parameters of the triangle are not valid");
});
#endregion
// но мне показалось, что будет правильным расширить это понятие и реализовать вычисление площади фигуры, без упоминания названия фигуры.
// Т.е. её определение в процессе выполнения программы (в зависимости от количества параметров)
// Данную реализацию я демонстрирую чуть ниже

// Так как проверка на прямоугольность треугольника очень специфичный случай, не подходящий к остальным фигурам, его я решил не вписывать 
// в функциональность Посетителя(Visitor), поэтому мне пришлось создать еще один частный случай
#region Второй подход

app.MapGet("/CalculateArea/{sideA}/{sideB?}/{sideC?}", async (HttpContext context, double sideA, double? sideB, double? sideC) =>
{
    var classifiedFigure = new FigureClassifier().Classify(sideA, sideB, sideC);
    double area;

    if (classifiedFigure != null)
    {
        area = classifiedFigure.CalculateArea(visitor);

        await context.Response.WriteAsync($"The area of the {classifiedFigure.GetType().Name} is: {area} \n");

        if (classifiedFigure is Triangle)
        {
            Triangle triangle = (Triangle)classifiedFigure;
            bool isRightTriangle = triangle.CheckIfTriangleIsRight();

            await context.Response.WriteAsync($"Is the Triangle right-angled? - {isRightTriangle}");
        }
    }
    else
        await context.Response.WriteAsync($"Input parameters are not valid");
});

#endregion

app.Run();

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

// ����� � �� �����, ����� ���������� �� ��������, ����� ���������� 3 �����: "���������� ������� ������ ��� ������ ���� ������ � compile-time"
// � ����������������, ��������������� ������� ������������� ���� ����������
#region ������ ������

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
// �� ��� ����������, ��� ����� ���������� ��������� ��� ������� � ����������� ���������� ������� ������, ��� ���������� �������� ������.
// �.�. � ����������� � �������� ���������� ��������� (� ����������� �� ���������� ����������)
// ������ ���������� � ������������ ���� ����

// ��� ��� �������� �� ��������������� ������������ ����� ����������� ������, �� ���������� � ��������� �������, ��� � ����� �� ��������� 
// � ���������������� ����������(Visitor), ������� ��� �������� ������� ��� ���� ������� ������
#region ������ ������

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

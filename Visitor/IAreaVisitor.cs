namespace TestTaskMindbox
{
    public interface IAreaVisitor
    {
        double Visit(Circle circle);
        double Visit(Triangle triangle);
    }
}

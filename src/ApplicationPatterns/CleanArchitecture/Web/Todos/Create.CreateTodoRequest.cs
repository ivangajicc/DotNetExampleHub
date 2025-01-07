namespace CleanArchitecture.Web.Todos;

public class CreateTodoRequest
{
    public const string Route = "/Todos";

    public string Description { get; set; } = null!;
}

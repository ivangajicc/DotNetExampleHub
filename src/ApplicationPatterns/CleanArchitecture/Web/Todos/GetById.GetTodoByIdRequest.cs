namespace CleanArchitecture.Web.Todos;

public class GetTodoByIdRequest
{
    public const string Route = "/Todos/{TodoId:Guid}";

    public Guid TodoId { get; set; }

    public static string BuildRoute(Guid todoId) => Route.Replace("{TodoId:Guid}", todoId.ToString());
}

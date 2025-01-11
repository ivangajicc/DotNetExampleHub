using System.Globalization;

namespace CleanArchitecture.Web.Todos;

public class MarkTodoAsResolvedRequest
{
    public const string Route = "/Todos/{TodoId:Guid}/resolve";

    public Guid TodoId { get; set; }

    public static string BuildRoute(Guid todoId) => Route.Replace("{TodoId:Guid}", todoId.ToString());

}

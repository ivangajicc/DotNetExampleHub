namespace CleanArchitecture.Web.Todos;

public record TodoRecord(Guid Id, bool IsResolved, string Description);

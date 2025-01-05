namespace DesignPatterns.TemplateMethod;

public record class SearchResult(
    int SearchedNumber,
    string Name,
    bool Found,
    int? Index);
